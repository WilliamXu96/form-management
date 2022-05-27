using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
using XCZ.Permissions;
using XCZ.Utilities;

namespace XCZ.FormBuildManagement
{
    [Authorize(FormPermissions.FormBuild.Default)]
    public class FormBuildAppService : ApplicationService, IFormBuildAppService
    {
        private readonly IRepository<Form, Guid> _formRep;
        private readonly IRepository<FormField, Guid> _fieldRep;
        private IWebHostEnvironment _hostEnvironment;

        public FormBuildAppService(
            IRepository<Form, Guid> formRep,
            IRepository<FormField, Guid> columnRep,
            IWebHostEnvironment hostingEnvironment)
        {
            _formRep = formRep;
            _fieldRep = columnRep;
            _hostEnvironment = hostingEnvironment;
        }

        [Authorize(FormPermissions.FormBuild.Build)]
        public async Task Build(Guid id)
        {
            if (!_hostEnvironment.IsDevelopment())
                throw new BusinessException("仅限开发环境使用！");

            var form = await _formRep.GetAsync(id);
            var fields = await (await _fieldRep.GetQueryableAsync()).Where(_ => _.FormId == form.Id).ToListAsync();
            var parentPath = new DirectoryInfo(_hostEnvironment.ContentRootPath).Parent.FullName;
            new CodeBuild.CodeBuildHelper(form, fields, parentPath, SystemSymbolHelper.GetSysPathSeparator(), SystemSymbolHelper.GetSysLineFeed()).Build();
        }

        [Authorize(FormPermissions.FormBuild.Download)]
        public async Task<string> Download(Guid id)
        {
            var form = await _formRep.GetAsync(id);
            var fields = await (await _fieldRep.GetQueryableAsync()).Where(_ => _.FormId == form.Id).ToListAsync();
            //TODO：path验证
            var parentPath = Path.Combine(_hostEnvironment.WebRootPath, form.Namespace);
            var file = parentPath + ".zip";
            //TODO：删除
            new CodeBuild.CodeBuildHelper(form, fields, parentPath, SystemSymbolHelper.GetSysPathSeparator(), SystemSymbolHelper.GetSysLineFeed()).Build();
            if(FileHelper.FileExists(file))
            {
                FileHelper.FileDel(file);
            }
            ZipFile.CreateFromDirectory(parentPath, file, CompressionLevel.Fastest, true);
            return file;
        }

        public async Task<FormBuildDto> Get(Guid id)
        {
            var form = await _formRep.GetAsync(id);
            var columns = await (await _fieldRep.GetQueryableAsync()).Where(_ => _.FormId == id).OrderBy(_ => _.FieldOrder).ToListAsync();
            var dto = ObjectMapper.Map<Form, FormBuildDto>(form);
            dto.Fields = ObjectMapper.Map<List<FormField>, List<FormFieldDto>>(columns);
            return dto;
        }

        public async Task<PagedResultDto<FormBuildDto>> GetAll(GetFormInputDto input)
        {
            var query = (await _formRep.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.FormName.Contains(input.Filter));
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "FormName").Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            var dto = ObjectMapper.Map<List<Form>, List<FormBuildDto>>(items);
            return new PagedResultDto<FormBuildDto>(totalCount, dto);
        }

        [Authorize(FormPermissions.FormBuild.Update)]
        public async Task<FormBuildDto> Update(Guid id, UpdateFormDto input)
        {
            var form = await _formRep.GetAsync(id);
            form.Namespace = input.Namespace.FirstCharToUpper();
            form.EntityName = input.EntityName;
            form.TableName = input.TableName;
            form.Remark = input.Remark;
            var fields = await _fieldRep.GetListAsync(_ => _.FormId == id);
            foreach (var field in fields)
            {
                var i = input.Fields.First(_ => _.Id == field.Id);
                field.Label = i.Label;
                field.DataType = i.DataType;
                field.IsReadonly = i.IsReadonly;
                field.IsRequired = i.IsRequired;
                field.IsIndex = i.IsIndex;
            }
            await _fieldRep.UpdateManyAsync(fields);
            return ObjectMapper.Map<Form, FormBuildDto>(form);
        }

    }

}
