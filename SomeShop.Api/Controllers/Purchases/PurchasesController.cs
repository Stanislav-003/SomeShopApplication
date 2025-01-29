using MediatR;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Api.Controllers.Users;
using SomeShop.Application.Purchases.CreatePurchase;
using SomeShop.Application.Users.CreateUser;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Api.Controllers.Purchases;

[ApiController]
[Route("api/purchases")]
public class PurchasesController : ControllerBase
{
    private readonly ISender _sender;

    public PurchasesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchase(
        [FromBody] CreatePurchaseRequest createPurchaseRequest,
        CancellationToken cancellationToken)
    {
        var command = new CreatePurchaseCommand(createPurchaseRequest.UserId, createPurchaseRequest.Items);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
