using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XCZ.FormDataManagement.Dto;
using XCZ.FormManagement;

namespace XCZ.FormDataManagement
{
    public class FormDataAppService : ApplicationService, IFormDataAppService
    {
        private readonly IRepository<FormData, Guid> _repository;

        public FormDataAppService(
            IRepository<FormData, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<FormDataDto> Create(CreateOrUpdateFormDataDto input)
        {
            var data = JsonConvert.SerializeObject(input.Data);
            var result = await _repository.InsertAsync(new FormData(GuidGenerator.Create())
            {
                FormId = input.FormId,
                Data = data
            });
            return ObjectMapper.Map<FormData, FormDataDto>(result);
        }

        public async Task<Dictionary<string, string>> Get(Guid id)
        {
            var formData = await _repository.GetAsync(id);
            var dataItems = JsonConvert.DeserializeObject<List<FormDataItemDto>>(formData.Data);
            var result = new Dictionary<string, string>();
            result.Add("id", formData.Id.ToString());
            foreach (var dataItem in dataItems)
            {
                result.Add(dataItem.fieldName, dataItem.value);
            }
            return result;
        }

        public async Task<PagedResultDto<Dictionary<string, string>>> GetAll(GetFormDataInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).Where(_ => _.FormId == input.FormId)
                                                               .WhereIf(!input.Filter.IsNullOrWhiteSpace(), _ => _.Data.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "CreationTime DESC")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var resultData = new List<Dictionary<string, string>>();
            foreach (var item in items)
            {
                var dataItems = JsonConvert.DeserializeObject<List<FormDataItemDto>>(item.Data);
                var dic = new Dictionary<string, string>();
                dic.Add("id", item.Id.ToString());
                foreach (var dataItem in dataItems)
                {
                    dic.Add(dataItem.fieldName, dataItem.value);
                }
                resultData.Add(dic);
            }
            return new PagedResultDto<Dictionary<string, string>>(totalCount, resultData);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<FormDataDto> Update(Guid id, CreateOrUpdateFormDataDto input)
        {
            var result = await _repository.GetAsync(id);
            if (result.FormId != input.FormId) throw new BusinessException("修改失败：表单数据异常");
            var data = JsonConvert.SerializeObject(input.Data);
            result.Data = data;
            return ObjectMapper.Map<FormData, FormDataDto>(result);
        }
    }
}
