using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Core.Models;
using MyNotes.Persistence.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return _mapper.Map<User>(userEntity);
        }
    }
}
