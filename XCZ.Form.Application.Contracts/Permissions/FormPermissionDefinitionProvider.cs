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
            var formManagement = context.AddGroup(FormPermissions.FormManagement, L("表单管理"), MultiTenancySides.Tenant);

            var form=formManagement.AddPermission(FormPermissions.Form.Default, L("表单"));
            form.AddChild(FormPermissions.Form.Update, L("Edit"));
            form.AddChild(FormPermissions.Form.Delete, L("Delete"));
            form.AddChild(FormPermissions.Form.Create, L("Create"));

            var formBuild = formManagement.AddPermission(FormPermissions.FormBuild.Default, L("代码生成"));
            formBuild.AddChild(FormPermissions.FormBuild.Update, L("配置"));
            formBuild.AddChild(FormPermissions.FormBuild.Build, L("生成"));
            formBuild.AddChild(FormPermissions.FormBuild.Download, L("下载"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FormResource>(name);
        }
    }
}
