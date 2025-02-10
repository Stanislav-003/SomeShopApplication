using MediatR;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Application.Users.CreateUser;
using SomeShop.Application.Users.GetUserProducts;
using SomeShop.Application.Users.GetUsersByBirthday;
using SomeShop.Application.Users.GetUsersForNDays;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Api.Controllers.Users;

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

    [HttpGet("getUserByBirthdayToday")]
    public async Task<IActionResult> GetUsersByBirthday(
        CancellationToken cancellationToken)
    {
        var query = new GetUsersByBirthdayQuery();

        Result<IEnumerable<GetUsersByBirthdayResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("getUsersPurchaseForNdays")]
    public async Task<IActionResult> GetUsersPurchaseForNDays(
        [FromQuery] GetUsersForNDaysRequest getUsersForNDaysRequest,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersForNDaysQuery(getUsersForNDaysRequest.nDays);

        Result<IEnumerable<GetUsersForNDaysResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("getUserProductsCategoriesById")]
    public async Task<IActionResult> GetUserProductsCategoriesById(
        [FromQuery] GetUserProductsCategoriesByIdRequest getUserProductsCategoriesByIdRequest,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProductsQuery(getUserProductsCategoriesByIdRequest.UserId);

        Result<IEnumerable<UserProductsResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }
}
