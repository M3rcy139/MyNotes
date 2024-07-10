using AutoMapper;
using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Core.Models;
using MyNotes.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyNotes.Persistence.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly NotesDbContext _context;
        private IMapper _mapper;

        public NotesRepository(NotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(Note note)
        {
            var noteEntity = new NoteEntity()
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Notes.AddAsync(noteEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> Get(string search, string sortItem, string sortOrder, CancellationToken ct)
        {
            var notesQuery = _context.Notes
                .Where(n => string.IsNullOrWhiteSpace(search) ||
                    n.Title.ToLower().Contains(search.ToLower()));

            Expression<Func<NoteEntity, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => note => note.CreatedAt,
                "title" => note => note.Title,
                _ => note => note.Id
            };

            notesQuery = sortOrder == "desc"
                ? notesQuery = notesQuery.OrderByDescending(selectorKey)
                : notesQuery = notesQuery.OrderBy(selectorKey);

            var noteEntities = await notesQuery
                .Select(n => new NoteEntity
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync(ct);

            return _mapper.Map<List<Note>>(noteEntities);
        }
    }
}
