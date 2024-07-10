using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Core.Models;

namespace MyNotes.Application.Services
{
    public class NotesService
    {
        private readonly INotesRepository _noteRepository;
        public NotesService(INotesRepository noteRepository)
        { 
            _noteRepository = noteRepository;
        }

        public async Task CreateNote(Note note)
        {
            await _noteRepository.Create(note);
        }

        public async Task<List<Note>> GetNote(string search, string sortItem, string sortOrder, CancellationToken ct)
        {
           return await _noteRepository.Get(search, sortItem, sortOrder, ct);
        }
    }
}
