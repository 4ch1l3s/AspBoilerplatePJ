using System.Collections.Generic;
using internPJ3.Roles.Dto;

namespace internPJ3.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
