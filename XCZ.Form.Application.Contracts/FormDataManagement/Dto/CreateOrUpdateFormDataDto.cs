using System;
using System.Collections.Generic;

namespace XCZ.FormDataManagement.Dto
{
    public class CreateOrUpdateFormDataDto
    {
        public Guid FormId { get; set; }

        public List<FormDataItemDto> Data { get; set; }
    }
}
