
using MyNotes.Core.Enums;

namespace MyNotes.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string userName, string email, string password, Role role);
    }
}
