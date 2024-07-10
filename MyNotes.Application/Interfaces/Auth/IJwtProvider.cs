using MyNotes.Core.Models;

namespace MyNotes.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
