using MyNotes.Application.Interfaces.Auth;
using MyNotes.Core.Models;
using MyNotes.Core.Interfaces.Repositories;
using MyNotes.Core.Interfaces.Services;
using MyNotes.Core.Enums;

namespace MyNotes.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            IJwtProvider jwtProvider,
            IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher)
        {
            _jwtProvider = jwtProvider;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher; 
        }

        public async Task Register(string userName, string password, string email, Role role)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword , email);

            await _usersRepository.Add(user, role);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
