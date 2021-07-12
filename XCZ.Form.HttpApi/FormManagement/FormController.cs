using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XCZ.FormManagement.Dto;

namespace XCZ.FormManagement
{
    [RemoteService]
    [Area("business")]
    [Route("api/business/form")]
    public class FormController : AbpController, IFormAppService
    {
        private readonly IFormAppService _formAppService;

        public FormController(IFormAppService formAppService)
        {
            _formAppService = formAppService;
        }

        [HttpPost]
        public Task<FormDto> Create(CreateOrUpdateFormDto input)
        {
            return _formAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _formAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<FormDto> Get(Guid id)
        {
            return _formAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<FormDto>> GetAll(GetFormInputDto input)
        {
            return _formAppService.GetAll(input);
        }

        [HttpGet]
        [Route("list")]
        public Task<ListResultDto<FormListDto>> GetAllList()
        {
            return _formAppService.GetAllList();
        }

        [HttpGet]
        [Route("field/{id}")]
        public Task<ListResultDto<FormFieldListDto>> GetField(Guid id)
        {
            return _formAppService.GetField(id);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<FormDto> Update(Guid id, CreateOrUpdateFormDto input)
        {
            return _formAppService.Update(id, input);
        }
    }
}
