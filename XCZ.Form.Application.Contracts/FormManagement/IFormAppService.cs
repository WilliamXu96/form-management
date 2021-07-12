using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XCZ.FormManagement.Dto;

namespace XCZ.FormManagement
{
    public interface IFormAppService : IApplicationService
    {
        Task<FormDto> Create(CreateOrUpdateFormDto input);

        Task Delete(List<Guid> ids);

        Task<FormDto> Update(Guid id, CreateOrUpdateFormDto input);

        Task<PagedResultDto<FormDto>> GetAll(GetFormInputDto input);

        Task<FormDto> Get(Guid id);

        Task<ListResultDto<FormListDto>> GetAllList();

        Task<ListResultDto<FormFieldListDto>> GetField(Guid id);
    }
}
