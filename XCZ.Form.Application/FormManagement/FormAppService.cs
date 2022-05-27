using Microsoft.AspNetCore.Authorization;
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
using XCZ.Permissions;

namespace XCZ.FormManagement
{
    [Authorize(FormPermissions.Form.Default)]
    public class FormAppService : ApplicationService, IFormAppService
    {
        private readonly IRepository<Form, Guid> _formRep;
        private readonly IRepository<FormField, Guid> _columnRep;
        private readonly IRepository<FormFieldOption, Guid> _fieldOptRep;

        public FormAppService(
            IRepository<Form, Guid> formRep,
            IRepository<FormField, Guid> columnRep,
            IRepository<FormFieldOption, Guid> fieldOptRep)
        {
            _formRep = formRep;
            _columnRep = columnRep;
            _fieldOptRep = fieldOptRep;
        }

        [Authorize(FormPermissions.Form.Create)]
        public async Task<FormDto> Create(CreateOrUpdateFormDto input)
        {
            var id = GuidGenerator.Create();
            var form = await _formRep.InsertAsync(new Form(id, CurrentTenant.Id, input.Api, input.FormName, input.DisplayName, input.Description, input.Disabled));
            await FieldInsert(id, input.Fields);
            return ObjectMapper.Map<Form, FormDto>(form);
        }

        [Authorize(FormPermissions.Form.Delete)]
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
            var columns = await (await _columnRep.GetQueryableAsync()).Where(_ => _.FormId == id)
                                          .OrderBy(_ => _.FieldOrder)
                                          .ToListAsync();
            var opts = await _fieldOptRep.GetListAsync(_ => _.FormId == id);
            var dto = ObjectMapper.Map<Form, FormDto>(form);
            dto.Fields = ObjectMapper.Map<List<FormField>, List<FormFieldDto>>(columns);
            foreach (var f in dto.Fields)
            {
                var opt = opts.Where(_ => _.FormFieldId == f.Id).ToList();
                if (opt.Any()) f.Options = ObjectMapper.Map<List<FormFieldOption>, List<FieldOption>>(opt);
            }
            return dto;
        }

        public async Task<PagedResultDto<FormDto>> GetAll(GetFormInputDto input)
        {
            var query = (await _formRep.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.FormName.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "FormName")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dto = ObjectMapper.Map<List<Form>, List<FormDto>>(items);
            return new PagedResultDto<FormDto>(totalCount, dto);
        }

        [Authorize(FormPermissions.Form.Update)]
        public async Task<FormDto> Update(Guid id, CreateOrUpdateFormDto input)
        {
            var form = await _formRep.GetAsync(id);
            form.FormName = input.FormName;
            form.DisplayName = input.DisplayName;
            form.Api = input.Api;
            form.Disabled = input.Disabled;
            form.Description = input.Description;

            await _columnRep.DeleteAsync(_ => _.FormId == id);
            await _fieldOptRep.DeleteAsync(_ => _.FormId == id);
            await FieldInsert(id, input.Fields);
            return ObjectMapper.Map<Form, FormDto>(form);
        }

        public async Task<ListResultDto<FormListDto>> GetAllList()
        {
            var list = await (from f in (await _formRep.GetQueryableAsync())
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
            var list = await (from c in (await _columnRep.GetQueryableAsync())
                              where c.FormId == id
                              select new FormFieldListDto
                              {
                                  Id = c.Id,
                                  FieldName = c.FieldName,
                                  Label = c.Label
                              }).ToListAsync();
            return new ListResultDto<FormFieldListDto>(list);
        }

        private async Task FieldInsert(Guid pid, List<Field> fields)
        {
            foreach (var field in fields)
            {
                var id = GuidGenerator.Create();
                await _columnRep.InsertAsync(new FormField(id)
                {
                    TenantId = CurrentTenant.Id,
                    FormId = pid,
                    FieldType = field.FieldType,
                    DataType = StringExtension.ToColumnType(field.FieldType),
                    FieldName = field.FieldName,
                    Label = field.Label,
                    Placeholder = field.Placeholder,
                    DefaultValue = field.DefaultValue,
                    FieldOrder = field.FieldOrder,
                    Icon = field.Icon,
                    Maxlength = field.Maxlength,
                    IsReadonly = field.IsReadonly,
                    IsRequired = field.IsRequired,
                    IsIndex = field.IsIndex,
                    IsSort = field.IsSort,
                    Disabled = field.Disabled,
                    Span = field.Span,
                    Regx = field.Regx
                });
                if (field.Options != null) await OptionInsert(pid, id, field.Options);
            }
        }

        private async Task OptionInsert(Guid fid, Guid ffid, List<FieldOption> opts)
        {
            foreach (var o in opts) { await _fieldOptRep.InsertAsync(new FormFieldOption(GuidGenerator.Create()) { FormId = fid, FormFieldId = ffid, Label = o.Label, Value = o.Value }); }
        }
    }
}
