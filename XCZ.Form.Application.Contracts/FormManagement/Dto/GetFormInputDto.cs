using Volo.Abp.Application.Dtos;

namespace XCZ.FormManagement.Dto
{
    public class GetFormInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
