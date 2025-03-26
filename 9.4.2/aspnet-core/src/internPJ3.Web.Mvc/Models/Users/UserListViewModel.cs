using System.Collections.Generic;
using internPJ3.Roles.Dto;

namespace internPJ3.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
