using MyNotes.Core.Models;

namespace MyNotes.Application.Interfaces.Repositories
{
    public interface INotesRepository
    {
        Task Create(Note note);
        Task<List<Note>> Get(Guid userNoteId, string search, string sortItem, string sortOrder, CancellationToken ct);
        Task<List<Note>> GetAll(string search, string sortItem, string sortOrder, CancellationToken ct);
        Task Update(Guid id, string title, string description);
        Task Delete(Guid id);
    }
}
