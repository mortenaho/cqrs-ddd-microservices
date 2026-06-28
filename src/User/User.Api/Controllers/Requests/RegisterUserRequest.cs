namespace User.Api.Controllers.Requests;

public sealed record RegisterUserRequest(string Email, string FullName);
