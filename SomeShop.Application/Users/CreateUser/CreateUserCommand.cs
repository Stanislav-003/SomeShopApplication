using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.CreateUser;

public record CreateUserCommand(
    string firstName,
    string lastName,
    DateTime dateOfBirth) : ICommand<UserId>;
