using SomeShop.Application.Abstractions.Messaging;

namespace SomeShop.Application.Users.GetUserProducts;

public record GetUserProductsQuery(Guid userId) : IQuery<IEnumerable<UserProductsResponse>>;