using MyNotes.Core.Enums;
using MyNotes.Core.Models;

namespace MyNotes.Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user, Role role);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid userId);
    }
}
