using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace XCZ.FormManagement.Dto
{
    public class FormFieldDto : EntityDto<Guid>
    {
        public string FieldType { get; set; }

        public string FieldName { get; set; }

        public string DataType { get; set; }

        public string Label { get; set; }

        public string Placeholder { get; set; }

        public string DefaultValue { get; set; }

        public int FieldOrder { get; set; }

        public string Icon { get; set; }

        public int? Maxlength { get; set; }

        public bool IsReadonly { get; set; }

        public bool IsRequired { get; set; }

        public bool IsIndex { get; set; }

        public bool IsSort { get; set; }

        public bool Disabled { get; set; }

        public List<FieldReg> RegList { get; set; }

        public List<FieldOption> Options { get; set; }
    }
}
