using System;
using Volo.Abp.Application.Dtos;

namespace XCZ.FormManagement.Dto
{
    public class FormListDto : EntityDto<Guid>
    {
        public string FormName { get; set; }

        public string DisplayName { get; set; }
    }
}
