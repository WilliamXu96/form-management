using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace XCZ.FormManagement
{
    public class Form : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string Api { get; set; }

        public string FormName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Disabled { get; set; }

        public bool IsDeleted { get; set; }

        #region  >C#属性<
        public string Namespace { get; set; }

        #endregion

        #region  >Entity属性<
        public string EntityName { get; set; }

        public string TableName { get; set; }

        public string Remark { get; set; }

        #endregion

        protected Form()
        {

        }

        public Form(Guid id, Guid? tenantId,string api, string formName,string displayName, string description, bool disabled) : base(id)
        {
            TenantId = tenantId;
            Api = api;
            FormName = formName;
            DisplayName = displayName;
            Description = description;
            Disabled = disabled;
        }
    }
}
