using MyNotes.Core.Enums;
using MyNotes.Core.Models;

namespace MyNotes.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user, Role role);
        Task<User> GetByEmail(string email);
        Task<HashSet<Enums.Permission>> GetUserPermissions(Guid userId);
    }
}
