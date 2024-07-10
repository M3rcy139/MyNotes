using MyNotes.Core.Models;

namespace MyNotes.Application.Interfaces.Repositories
{
    public interface INotesRepository
    {
        Task Create(Note note);
        Task <List<Note>> Get(string serach, string sortItem, string sortOrder, CancellationToken ct);
    }
}
