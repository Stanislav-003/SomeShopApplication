using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.CreateUser;

public record CreateUserCommand(
    FirstName firstName,
    LastName lastName,
    DateTime dateOfBirth) : ICommand<Guid>;
