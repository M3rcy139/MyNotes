using MyNotes.Core.Models;


namespace MyNotes.Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);

        Task<User> GetByEmail(string email);

    }
}
