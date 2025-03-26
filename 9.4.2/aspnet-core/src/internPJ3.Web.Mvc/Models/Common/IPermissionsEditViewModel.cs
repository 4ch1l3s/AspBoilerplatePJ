using System.Collections.Generic;
using internPJ3.Roles.Dto;

namespace internPJ3.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}