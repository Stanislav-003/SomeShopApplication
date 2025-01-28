using MediatR;
using SomeShop.Domain.Abstractions;

namespace SomeShop.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
