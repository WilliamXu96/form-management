using System;
using Volo.Abp.Application.Dtos;

namespace XCZ.FormDataManagement.Dto
{
    public class GetFormDataInputDto : PagedAndSortedResultRequestDto
    {
        public Guid FormId { get; set; }

        public string Filter { get; set; }
    }
}
