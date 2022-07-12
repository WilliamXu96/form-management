using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace XCZ.FormManagement
{
    public class FormData : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid FormId { get; set; }

        public string Data { get; set; }

        public bool IsDeleted { get; set; }

        public FormData(Guid id) :base(id)
        {

        }
    }
}
