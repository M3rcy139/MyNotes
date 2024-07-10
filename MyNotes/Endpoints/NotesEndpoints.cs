using Microsoft.AspNetCore.Mvc;
using MyNotes.Contracts;
using MyNotes.Application.Services;
using MyNotes.Core.Models;

namespace MyNotes.Endpoints
{
    public static class NotesEndpoints
    {
        public static IEndpointRouteBuilder MapNotesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("notes").RequireAuthorization();

            endpoints.MapPost("create", Create);

            endpoints.MapGet("get", Get);

            return endpoints;
        }

        private static async Task<IResult> Create([FromBody] CreateNoteRequest request, 
            CancellationToken ct, NotesService noteService)
        {
            var note = new Note(request.Title, request.Description);

            await noteService.CreateNote(note);

            return Results.Ok();
        }

        private static async Task<IResult> Get([FromQuery] string? search, [FromQuery] string? sortItem, [FromQuery] string? sortOrder,
            CancellationToken ct, NotesService noteService)
        {
            var notes = await noteService.GetNote(search, sortItem, sortOrder, ct);

            var response = notes 
                .Select(n => new GetNotesResponse(n.Id, n.Title, n.Description, n.CreatedAt));

            return Results.Ok(response);
        }
    }
}

