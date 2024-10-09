using AutoMapper;
using MyNotes.Core.Models;
using MyNotes.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MyNotes.Application.Interfaces.Repositories;

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
                UserNoteId = note.UserNoteId,
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Notes.AddAsync(noteEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> Get(Guid userNoteId, string search, string sortItem, string sortOrder, CancellationToken ct)
        {
            IQueryable<NoteEntity> notesQuery = GetQuery(search, sortItem, sortOrder);

            var noteEntities = await notesQuery
                .Where(n => n.UserNoteId == userNoteId)
                .Select(n => n)
                .ToListAsync(ct);

            if (noteEntities == null || !noteEntities.Any())
            {
                throw new ArgumentException("The notes do not exist for the given user.");
            }

            return _mapper.Map<List<Note>>(noteEntities);
        }

        public async Task<List<Note>> GetAll(string search, string sortItem, string sortOrder, CancellationToken ct)
        {
            IQueryable<NoteEntity> notesQuery = GetQuery(search, sortItem, sortOrder);

            var noteEntities = await notesQuery
                .Select(n => n)
                .ToListAsync(ct);

            if (noteEntities == null || !noteEntities.Any())
            {
                throw new ArgumentException("The notes do not exist for the given user.");
            }

            return _mapper.Map<List<Note>>(noteEntities);
        }

        private IQueryable<NoteEntity> GetQuery(string search, string sortItem, string sortOrder)
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
            return notesQuery;
        }

        public async Task Update(Guid id, string title, string description)
        {
            var noteExists = await _context.Notes.AnyAsync(n => n.Id == id);
            if (!noteExists)
            {
                throw new ArgumentException($"Note with id {id} does not exist.");
            }

            await _context.Notes
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(n => n.Title, title)
                    .SetProperty(n => n.Description, description));
        }
        public async Task Delete(Guid id)
        {
            var noteExists = await _context.Notes.AnyAsync(n => n.Id == id);
            if (!noteExists)
            {
                throw new ArgumentException($"Note with id {id} does not exist.");
            }

            await _context.Notes
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
