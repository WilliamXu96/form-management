using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XCZ.FormBuildManagement.Dto;
using XCZ.FormManagement.Dto;

namespace XCZ.FormBuildManagement
{
    public interface IFormBuildAppService : IApplicationService
    {
        Task<FormBuildDto> Update(Guid id, UpdateFormDto input);

        Task<PagedResultDto<FormBuildDto>> GetAll(GetFormInputDto input);

        Task<FormBuildDto> Get(Guid id);

        Task Build(Guid id);
    }
}
