using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XCZ.Extensions;
using XCZ.FormManagement.Dto;

namespace XCZ.FormManagement
{
    public class FormAppService : ApplicationService, IFormAppService
    {
        private readonly IRepository<Form, Guid> _formRep;
        private readonly IRepository<FormField, int> _columnRep;

        public FormAppService(
            IRepository<Form, Guid> formRep,
            IRepository<FormField, int> columnRep)
        {
            _formRep = formRep;
            _columnRep = columnRep;
        }

        public async Task<FormDto> Create(CreateOrUpdateFormDto input)
        {
            var id = GuidGenerator.Create();
            var form = await _formRep.InsertAsync(new Form(id,
                                                           CurrentTenant.Id,
                                                           input.Api,
                                                           input.FormName,
                                                           input.DisplayName,
                                                           input.Description,
                                                           input.Disabled));

            foreach (var field in input.Fields)
            {
                await _columnRep.InsertAsync(new FormField(CurrentTenant.Id,
                                                           id,
                                                           field.FieldType,
                                                           StringExtension.ToColumnType(field.FieldType),
                                                           field.FieldName,
                                                           field.Label,
                                                           field.Placeholder,
                                                           field.DefaultValue,
                                                           field.FieldOrder,
                                                           field.Icon,
                                                           field.Maxlength,
                                                           field.IsReadonly,
                                                           field.IsRequired,
                                                           field.IsSort,
                                                           field.Disabled
                                                           //field.Regx,
                                                           //field.Options
                                                           ));
            }

            return ObjectMapper.Map<Form, FormDto>(form);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _formRep.DeleteAsync(id);
                await _columnRep.DeleteAsync(_ => _.FormId == id);
            }
        }

        public async Task<FormDto> Get(Guid id)
        {
            var form = await _formRep.GetAsync(id);
            var columns = await _columnRep.Where(_ => _.FormId == id)
                                          .OrderBy(_ => _.FieldOrder)
                                          .ToListAsync();

            var dto = ObjectMapper.Map<Form, FormDto>(form);
            dto.Fields = ObjectMapper.Map<List<FormField>, List<FormFieldDto>>(columns);
            return dto;
        }

        public async Task<PagedResultDto<FormDto>> GetAll(GetFormInputDto input)
        {
            var query = _formRep.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.FormName.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "FormName")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dto = ObjectMapper.Map<List<Form>, List<FormDto>>(items);
            return new PagedResultDto<FormDto>(totalCount, dto);
        }

        public async Task<FormDto> Update(Guid id, CreateOrUpdateFormDto input)
        {
            var form = await _formRep.GetAsync(id);
            form.FormName = input.FormName;
            form.Api = input.Api;
            form.Disabled = input.Disabled;

            await _columnRep.DeleteAsync(_ => _.FormId == id);
            foreach (var field in input.Fields)
            {
                await _columnRep.InsertAsync(new FormField(CurrentTenant.Id,
                                                           id,
                                                           field.FieldType,
                                                           StringExtension.ToColumnType(field.FieldType),
                                                           field.FieldName,
                                                           field.Label,
                                                           field.Placeholder,
                                                           field.DefaultValue,
                                                           field.FieldOrder,
                                                           field.Icon,
                                                           field.Maxlength,
                                                           field.IsReadonly,
                                                           field.IsRequired,
                                                           field.IsSort,
                                                           field.Disabled
                                                           //field.Regx,
                                                           //field.Options
                                                           ));
            }

            return ObjectMapper.Map<Form, FormDto>(form);
        }

        public async Task<ListResultDto<FormListDto>> GetAllList()
        {
            var list = await (from f in _formRep
                              select new FormListDto
                              {
                                  Id = f.Id,
                                  FormName = f.FormName,
                                  DisplayName = f.DisplayName
                              }).ToListAsync();
            return new ListResultDto<FormListDto>(list);
        }

        public async Task<ListResultDto<FormFieldListDto>> GetField(Guid id)
        {
            var list = await (from c in _columnRep
                              where c.FormId == id
                              select new FormFieldListDto
                              {
                                  Id = c.Id,
                                  FieldName = c.FieldName,
                                  Label = c.Label
                              }).ToListAsync();
            return new ListResultDto<FormFieldListDto>(list);
        }
    }
}
