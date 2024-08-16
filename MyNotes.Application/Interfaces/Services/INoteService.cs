using MyNotes.Core.Models;


namespace MyNotes.Application.Interfaces.Services
{
    public interface INoteService
    {
        Task CreateNote(Note note);

        Task<List<Note>> GetNote(Guid userNoteId, string search, string sortItem, string sortOrder, CancellationToken ct);

        Task<List<Note>> GetAllNotes(string search, string sortItem, string sortOrder, CancellationToken ct);
    }
}
