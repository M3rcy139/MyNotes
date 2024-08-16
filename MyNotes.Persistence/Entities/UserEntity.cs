

namespace MyNotes.Persistence.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<NoteEntity> Notes { get; set; } = [];
        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
