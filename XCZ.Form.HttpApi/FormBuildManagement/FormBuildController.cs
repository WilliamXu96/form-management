using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XCZ.FormBuildManagement.Dto;
using XCZ.FormManagement.Dto;

namespace XCZ.FormBuildManagement
{
    [RemoteService]
    [Area("business")]
    [Route("api/business/build")]
    public class FormBuildController : AbpController
    {
        private readonly IFormBuildAppService _formBuildAppService;

        public FormBuildController(IFormBuildAppService formBuildAppService)
        {
            _formBuildAppService = formBuildAppService;
        }

        [HttpPost]
        [Route("{id}")]
        public Task Build(Guid id)
        {
            return _formBuildAppService.Build(id);
        }

        [HttpGet]
        [Route("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var file = await _formBuildAppService.Download(id);
            return new FileContentResult(System.IO.File.ReadAllBytes(file), "application/zip")
            {
                FileDownloadName = Path.GetFileName(file)
            };
        }

        [HttpGet]
        [Route("{id}")]
        public Task<FormBuildDto> Get(Guid id)
        {
            return _formBuildAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<FormBuildDto>> GetAll(GetFormInputDto input)
        {
            return _formBuildAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<FormBuildDto> Update(Guid id, UpdateFormDto input)
        {
            return _formBuildAppService.Update(id, input);
        }
    }
}
