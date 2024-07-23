using MyNotes.Core.Interfaces.Services;
using MyNotes.Core.Enums;
using MyNotes.Core.Interfaces.Repositories;


namespace MyNotes.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
        { 
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}
