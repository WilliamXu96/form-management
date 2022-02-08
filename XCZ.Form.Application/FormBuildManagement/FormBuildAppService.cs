﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XCZ.Extensions;
using XCZ.FormBuildManagement.Dto;
using XCZ.FormManagement;
using XCZ.FormManagement.Dto;

namespace XCZ.FormBuildManagement
{
    public class FormBuildAppService : ApplicationService, IFormBuildAppService
    {
        private readonly IRepository<Form, Guid> _formRep;
        private readonly IRepository<FormField, Guid> _fieldRep;
        private IWebHostEnvironment _hostingEnvironment;

        public FormBuildAppService(
            IRepository<Form, Guid> formRep,
            IRepository<FormField, Guid> columnRep,
            IWebHostEnvironment hostingEnvironment)
        {
            _formRep = formRep;
            _fieldRep = columnRep;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Build(Guid id)
        {
            if (!_hostingEnvironment.IsDevelopment())
                throw new BusinessException("仅限开发环境使用！");

            var form = await _formRep.GetAsync(id);
            var fields = await (await _fieldRep.GetQueryableAsync()).Where(_ => _.FormId == form.Id).ToListAsync();
            var parentPath = new DirectoryInfo(_hostingEnvironment.ContentRootPath).Parent.FullName;
            new CodeBuild.CodeBuildHelper(form, fields, parentPath, SystemSymbolHelper.GetSysPathSeparator(), SystemSymbolHelper.GetSysLineFeed()).Build();
        }

        public async Task<FormBuildDto> Get(Guid id)
        {
            var form = await _formRep.GetAsync(id);
            var columns = await (await _fieldRep.GetQueryableAsync()).Where(_ => _.FormId == id)
                                         .OrderBy(_ => _.FieldOrder)
                                         .ToListAsync();

            var dto = ObjectMapper.Map<Form, FormBuildDto>(form);
            dto.Fields = ObjectMapper.Map<List<FormField>, List<FormFieldDto>>(columns);
            return dto;
        }

        public async Task<PagedResultDto<FormBuildDto>> GetAll(GetFormInputDto input)
        {
            var query = (await _formRep.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.FormName.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "FormName")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dto = ObjectMapper.Map<List<Form>, List<FormBuildDto>>(items);
            return new PagedResultDto<FormBuildDto>(totalCount, dto);
        }

        public async Task<FormBuildDto> Update(Guid id, UpdateFormDto input)
        {
            var form = await _formRep.GetAsync(id);
            form.Namespace = input.Namespace.FirstCharToUpper();
            form.EntityName = input.EntityName;
            form.TableName = input.TableName;
            form.Remark = input.Remark;

            await _fieldRep.DeleteAsync(_ => _.FormId == id);
            foreach (var field in input.Fields)
            {
                await _fieldRep.InsertAsync(new FormField(GuidGenerator.Create())
                {
                    TenantId = CurrentTenant.Id,
                    FormId = id,
                    FieldType = field.FieldType,
                    DataType = field.DataType,
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

                });
            }

            return ObjectMapper.Map<Form, FormBuildDto>(form);
        }

    }

}
