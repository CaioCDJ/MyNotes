namespace MyNotes.Endpoints.Users.Create;

public record Request(
  string name,
  string email,
  string password
);
