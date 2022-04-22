using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCZ.FormManagement.Dto
{
    public class CreateOrUpdateFormDto
    {
        public string Api { get; set; }

        [Required]
        [MaxLength(50)]
        public string FormName { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool Disabled { get; set; }

        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FieldType { get; set; }

        [Required]
        [MaxLength(50)]
        public string FieldName { get; set; }

        [MaxLength(20)]
        public string DataType { get; set; }

        [Required]
        [MaxLength(128)]
        public string Label { get; set; }

        [MaxLength(50)]
        public string Placeholder { get; set; }

        [MaxLength(256)]
        public string DefaultValue { get; set; }

        public int FieldOrder { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

        public int? Maxlength { get; set; }

        public bool IsReadonly { get; set; }

        public bool IsRequired { get; set; }

        public bool IsIndex { get; set; }

        public bool IsSort { get; set; }

        public bool Disabled { get; set; }

        public List<FieldReg> RegList { get; set; }

        public List<FieldOption> Options { get; set; }

        /// <summary>
        /// 布局时表单占用的栅格数
        /// XiangMingHuii
        /// </summary>
        public int Span { get; set; }
        /// <summary>
        /// 正则表达式
        /// XiangMingHuii
        /// </summary>
        public string Regx { get; set; }
    }

    public class FieldReg
    {
        public string Message { get; set; }

        public string Pattern { get; set; }
    }

    public class FieldOption
    {
        public string Label { get; set; }

        public string Value { get; set; }
    }
}
