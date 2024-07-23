using MyNotes.Core.Models;

namespace MyNotes.Core.Interfaces.Repositories
{
    public interface INotesRepository
    {
        Task Create(Note note);
        Task<List<Note>> Get(string serach, string sortItem, string sortOrder, CancellationToken ct);
    }
}
