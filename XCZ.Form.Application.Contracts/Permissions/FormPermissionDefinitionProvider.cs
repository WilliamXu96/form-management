using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using XCZ.Localization;

namespace XCZ.Permissions
{
    public class FormPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var business = context.AddGroup(FormPermissions.Form, L("Form"), MultiTenancySides.Tenant);

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FormResource>(name);
        }
    }
}
