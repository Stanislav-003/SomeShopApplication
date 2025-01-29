using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Application.Users.CreateUser;
using SomeShop.Application.Users.GetUsersByBirthday;
using SomeShop.Application.Users.GetUsersForNDays;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CreateUserRequest createUserRequest,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            createUserRequest.firstName,
            createUserRequest.lastName,
            createUserRequest.dateOfBirth);

        Result<UserId> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("getUserById")]
    public async Task<IActionResult> GetUsersByBirthday(
        CancellationToken cancellationToken)
    { 
        var query = new GetUsersByBirthdayQuery();

        Result<IReadOnlyCollection<GetUsersByBirthdayResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("getUsersPurchaseForNdays")]
    public async Task<IActionResult> GetUsersForNDays(
        [FromQuery] GetUsersForNDaysRequest getUsersForNDaysRequest,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersForNDaysQuery(getUsersForNDaysRequest.nDays);

        Result<IReadOnlyCollection<GetUsersForNDaysResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }
}
