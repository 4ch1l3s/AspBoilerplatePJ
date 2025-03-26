using Abp.AutoMapper;
using internPJ3.Roles.Dto;
using internPJ3.Web.Models.Common;

namespace internPJ3.Web.Models.Roles
{
	[AutoMapFrom(typeof(GetRoleForEditOutput))]
	public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
	{
		public bool HasPermission(FlatPermissionDto permission)
		{
			return GrantedPermissionNames.Contains(permission.Name);
		}
	}
}
