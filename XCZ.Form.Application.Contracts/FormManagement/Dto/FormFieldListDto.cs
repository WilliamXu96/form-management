using System;
using Volo.Abp.Application.Dtos;

namespace XCZ.FormManagement.Dto
{
    public class FormFieldListDto : EntityDto<Guid>
    {
        public string FieldName { get; set; }

        public string Label { get; set; }
    }
}
