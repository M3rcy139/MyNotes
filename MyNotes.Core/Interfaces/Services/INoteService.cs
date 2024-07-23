using MyNotes.Core.Models;


namespace MyNotes.Core.Interfaces.Services
{
    public interface INoteService
    {
        Task CreateNote(Note note);

        Task<List<Note>> GetNote(string search, string sortItem, string sortOrder, CancellationToken ct);
    }
}
