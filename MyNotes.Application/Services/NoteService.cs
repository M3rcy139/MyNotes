using MyNotes.Core.Models;
using MyNotes.Application.Interfaces.Services;
using MyNotes.Application.Interfaces.Repositories;

namespace MyNotes.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INotesRepository _noteRepository;
        public NoteService(INotesRepository noteRepository)
        { 
            _noteRepository = noteRepository;
        }

        public async Task CreateNote(Note note)
        {
            await _noteRepository.Create(note);
        }

        public async Task<List<Note>> GetNote(Guid userNoteId, string search, string sortItem, string sortOrder, CancellationToken ct)
        {
           return await _noteRepository.Get(userNoteId, search, sortItem, sortOrder, ct);
        }

        public async Task<List<Note>> GetAllNotes(string search, string sortItem, string sortOrder, CancellationToken ct)
        {
            return await _noteRepository.GetAll(search, sortItem, sortOrder, ct);
        }
    }
}
