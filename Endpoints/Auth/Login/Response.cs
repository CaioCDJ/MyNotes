namespace MyNotes.Endpoints.Auth.Login;

public record Response(
    string name,
    string email,
    string token
    );
