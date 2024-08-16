using MyNotes.Core.Enums;

namespace MyNotes.Application.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
    }
}
