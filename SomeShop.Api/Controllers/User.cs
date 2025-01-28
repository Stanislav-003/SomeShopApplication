using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SomeShop.Api.Controllers;

[ApiController]
[Route("api/users")]
public class User : ControllerBase
{
    private readonly ISender _sender;

    public User(ISender sender)
    {
        _sender = sender;
    }
}
