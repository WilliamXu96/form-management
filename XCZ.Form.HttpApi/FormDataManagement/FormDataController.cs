using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XCZ.FormDataManagement.Dto;

namespace XCZ.FormDataManagement
{
    [RemoteService]
    [Area("business")]
    [Route("api/business/form-data")]
    public class FormDataController : AbpController, IFormDataAppService
    {
        private readonly IFormDataAppService _formDataAppService;

        public FormDataController(IFormDataAppService formDataAppService)
        {
            _formDataAppService = formDataAppService;
        }

        [HttpPost]
        public Task<FormDataDto> Create(CreateOrUpdateFormDataDto input)
        {
            return _formDataAppService.Create(input);
        }

        [HttpGet]
        public Task<PagedResultDto<Dictionary<string, string>>> GetAll(GetFormDataInputDto input)
        {
            return _formDataAppService.GetAll(input);
        }
    }
}
