using MyNotes.Core.Models;

namespace MyNotes.Persistence.Entities
{
    public class NoteEntity
    {
        public Guid UserNoteId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserEntity? User { get; set; }
    }
}
