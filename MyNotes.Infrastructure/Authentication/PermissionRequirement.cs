using Microsoft.AspNetCore.Authorization;
using MyNotes.Core.Enums;

namespace MyNotes.Infrastructure.Authentication
{
    public class PermissionRequirement(Permission[] permissions) 
        : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = permissions;
    }
}
