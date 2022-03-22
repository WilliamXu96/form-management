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
            var formManagement = context.AddGroup(FormPermissions.FormManagement, L("FormManagement"), MultiTenancySides.Tenant);

            var form=formManagement.AddPermission(FormPermissions.Form.Default, L("Form"));
            form.AddChild(FormPermissions.Form.Update, L("Edit"));
            form.AddChild(FormPermissions.Form.Delete, L("Delete"));
            form.AddChild(FormPermissions.Form.Create, L("Create"));

            var formBuild = formManagement.AddPermission(FormPermissions.FormBuild.Default, L("FormBuild"));
            formBuild.AddChild(FormPermissions.FormBuild.Update, L("Edit"));
            formBuild.AddChild(FormPermissions.FormBuild.Build, L("Build"));
            formBuild.AddChild(FormPermissions.FormBuild.Create, L("Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FormResource>(name);
        }
    }
}
