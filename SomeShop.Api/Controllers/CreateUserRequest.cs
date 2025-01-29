namespace SomeShop.Api.Controllers;

public record CreateUserRequest(
    string firstName,
    string lastName,
    DateTime dateOfBirth);
