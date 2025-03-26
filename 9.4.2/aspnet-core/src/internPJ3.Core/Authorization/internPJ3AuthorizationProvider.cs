using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace internPJ3.Authorization
{
    public class internPJ3AuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Products, L("Product"));
            context.CreatePermission(PermissionNames.Pages_Category, L("Category"));
            context.CreatePermission(PermissionNames.Pages_Shopee, L("Shopee"));
			      context.CreatePermission(PermissionNames.Delete_Perm, L("Delete"));
			      context.CreatePermission(PermissionNames.Edit_Perm, L("Edit"));


			context.CreatePermission(PermissionNames.Nav_Tab, L("NavBar"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, internPJ3Consts.LocalizationSourceName);
        }
    }
}
