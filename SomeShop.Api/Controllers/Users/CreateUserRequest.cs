namespace SomeShop.Api.Controllers.Users;

public record CreateUserRequest(
    string firstName,
    string lastName,
    DateTime dateOfBirth);
