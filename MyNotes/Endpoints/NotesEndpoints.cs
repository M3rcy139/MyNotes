using Microsoft.AspNetCore.Mvc;
using MyNotes.Contracts;
using MyNotes.Application.Services;
using MyNotes.Core.Models;
using MyNotes.Extensions;
using MyNotes.Core.Enums;
using MyNotes.Application.Interfaces.Services;
using MyNotes.Application.Interfaces.Repositories;

namespace MyNotes.Endpoints
{
    public static class NotesEndpoints
    {
        public static IEndpointRouteBuilder MapNotesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("notes");

            endpoints.MapPost("create", Create).RequirePermissions(Permission.Create);

            endpoints.MapGet("get", Get).RequirePermissions(Permission.Read);

            endpoints.MapGet("getAll", GetAll).RequirePermissions(Permission.ReadAll);

            endpoints.MapPut("update", Update).RequirePermissions(Permission.Update);

            endpoints.MapDelete("delete", Delete).RequirePermissions(Permission.Delete);

            return endpoints;
        }

        private static async Task<IResult> Create([FromBody] CreateNoteRequest request, 
            CancellationToken ct, INoteService noteService,
            IUserService userService)
        {
            var userNoteId = userService.GetCurrentUserId();

            var note = new Note(userNoteId, request.Title, request.Description);

            await noteService.CreateNote(note);

            return Results.Ok();
        }

        private static async Task<IResult> Get([FromQuery] string? search, [FromQuery] string? sortItem, 
            [FromQuery] string? sortOrder,
            CancellationToken ct, INoteService noteService,
            IUserService userService)
        {
            var userNoteId = userService.GetCurrentUserId();
            var notes = await noteService.GetNote(userNoteId, search, sortItem, sortOrder, ct);

            var response = notes 
                .Select(n => new GetNotesResponse(n.Id, n.Title, n.Description, n.CreatedAt));

            return Results.Ok(response);
        }

        private static async Task<IResult> GetAll([FromQuery] string? search, [FromQuery] string? sortItem,
            [FromQuery] string? sortOrder, CancellationToken ct, INoteService noteService)
        {
            var notes = await noteService.GetAllNotes(search, sortItem, sortOrder, ct);

            var response = notes
                .Select(n => new GetNotesResponse(n.Id, n.Title, n.Description, n.CreatedAt));

            return Results.Ok(response);
        }

        public static async Task<IResult> Update(Guid id, [FromBody] CreateNoteRequest request, 
            INotesRepository notesRepository, CancellationToken ct)
        {
            await notesRepository.Update(id, request.Title, request.Description);

            return Results.Ok();
        }
        private static async Task<IResult> Delete(Guid id, INotesRepository notesRepository, CancellationToken ct)
        {
            await notesRepository.Delete(id);

            return Results.Ok();
        }
    }
}

