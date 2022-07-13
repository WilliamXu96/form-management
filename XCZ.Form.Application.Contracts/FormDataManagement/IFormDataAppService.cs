using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XCZ.FormDataManagement.Dto;

namespace XCZ.FormDataManagement
{
    public interface IFormDataAppService : IApplicationService
    {
        Task<FormDataDto> Create(CreateOrUpdateFormDataDto input);

        Task<PagedResultDto<Dictionary<string, string>>> GetAll(GetFormDataInputDto input);

        Task Delete(List<Guid> ids);

        Task<Dictionary<string, string>> Get(Guid id);

        Task<FormDataDto> Update(Guid id, CreateOrUpdateFormDataDto input);
    }
}
