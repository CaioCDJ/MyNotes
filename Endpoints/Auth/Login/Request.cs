namespace MyNotes.Endpoints.Auth.Login;

public record Request(
    string email,
    string password
    );
