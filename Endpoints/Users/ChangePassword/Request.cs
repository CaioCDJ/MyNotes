namespace MyNotes.Endpoints.Users.ChangePassword;

public record Request(
    string oldPassword,
    string newPassword,
    string confirmPasswor
    );
