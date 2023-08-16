namespace MyNotes.Endpoints.Notes.GetNotes;

public record Response(
    string id,
    string title,
    DateTime createdAt,
    DateTime? updatedAt
    );
