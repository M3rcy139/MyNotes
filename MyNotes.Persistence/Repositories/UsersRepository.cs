using MyNotes.Core.Models;
using MyNotes.Persistence.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Core.Enums;

namespace MyNotes.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(NotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Add(User user, Role role)
        {
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)role)
                ?? throw new ArgumentException($"Role with id {(int)role} does not exist.");

            var userExists = await _context.Users
                .AnyAsync(u => u.Email == user.Email);

            if (userExists)
            {
                throw new ArgumentException("A user with this email already exists.");
            }

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Roles = [roleEntity]
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentException($"User with email {email} was not found.");

            return _mapper.Map<User>(userEntity);
        }

        public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
    }
}
