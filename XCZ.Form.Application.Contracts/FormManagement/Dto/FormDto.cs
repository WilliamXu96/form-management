using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace XCZ.FormManagement.Dto
{
    public class FormDto : EntityDto<Guid>
    {
        public string Api { get; set; }

        public string FormName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Disabled { get; set; }

        public List<FormFieldDto> Fields { get; set; }
        public string Namespace { get; set; }
    }
}
