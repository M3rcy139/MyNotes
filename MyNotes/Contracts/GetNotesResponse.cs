namespace MyNotes.Contracts
{
    public record GetNotesResponse(
        Guid Id,
        string Title,
        string Description,
        DateTime CreatedAt);
}