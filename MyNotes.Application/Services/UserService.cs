using MyNotes.Application.Interfaces.Auth;
using MyNotes.Core.Models;
using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Application.Interfaces.Services;
using MyNotes.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyNotes.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IJwtProvider jwtProvider,
            IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _jwtProvider = jwtProvider;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher; 
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Register(string userName, string password, string email, Role role)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User(Guid.NewGuid(), userName, hashedPassword , email);

            await _usersRepository.Add(user, role);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            var token = _jwtProvider.Generate(user);

            return token;
        }

        public Guid GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return Guid.Parse(userIdClaim);
        }
    }
}
