using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace XCZ.FormManagement
{
    public class FormFieldOption : AggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid FormId { get; set; }

        public Guid FormFieldId { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }
    }
}
