using MyNotes.Core.Enums;

namespace MyNotes.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
    }
}
