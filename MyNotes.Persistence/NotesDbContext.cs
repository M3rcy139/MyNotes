using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyNotes.Persistence.Entities;

namespace MyNotes.Persistence
{
    public class NotesDbContext(DbContextOptions<NotesDbContext> options) : DbContext(options)
    {
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
