namespace MyNotes.Core.Models
{
    public class Note
    {
        public Note(Guid userNoteId, string title, string description)
        {
            UserNoteId = userNoteId;
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid UserNoteId { get; set; }
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
