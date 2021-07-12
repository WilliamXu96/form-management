using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace XCZ.FormManagement
{
    public class FormField : AggregateRoot<int>, IMultiTenant
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

        public string Options { get; set; }

        protected FormField()
        {

        }

        public FormField(Guid? tenantId,
                         Guid formId,
                         string fieldType,
                         string dataType,
                         string fieldName,
                         string label,
                         string placeholder,
                         string defaultValue,
                         int fieldOrder,
                         string icon,
                         int? maxlength,
                         bool isReadonly,
                         bool isRequire,
                         bool isSort,
                         bool disabled,
                         bool isIndex = false,
                         string regx = null,
                         string options = null)
        {
            TenantId = tenantId;
            FormId = formId;
            FieldType = fieldType;
            DataType = dataType;
            FieldName = fieldName;
            Label = label;
            Placeholder = placeholder;
            DefaultValue = defaultValue;
            FieldOrder = fieldOrder;
            Icon = icon;
            Maxlength = maxlength;
            IsReadonly = isReadonly;
            IsRequired = isRequire;
            IsSort = isSort;
            Disabled = disabled;
            IsIndex = isIndex;
            Regx = regx;
            Options = options;
        }
    }
}
