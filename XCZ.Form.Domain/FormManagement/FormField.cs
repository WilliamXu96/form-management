using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace XCZ.FormManagement
{
    public class FormField : AggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid FormId { get; set; }

        public string FieldType { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        public string FieldName { get; set; }

        public string Label { get; set; }

        public string Placeholder { get; set; }

        public string DefaultValue { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int FieldOrder { get; set; }

        public string Icon { get; set; }

        public int? Maxlength { get; set; }

        public bool IsReadonly { get; set; }

        public bool IsRequired { get; set; }

        public bool IsIndex { get; set; }

        //public bool IsDefaultSort { get; set; }

        public bool IsSort { get; set; }

        public bool Disabled { get; set; }

        public string Regx { get; set; }

        /// <summary>
        /// 布局时表单占用的栅格数
        /// XiangMingHuii
        /// </summary>
        public int Span { get; set; }

        public FormField(Guid id) : base(id)
        {

        }
    }
}
