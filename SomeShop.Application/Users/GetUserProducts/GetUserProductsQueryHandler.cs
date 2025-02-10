using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Application.Users.GetUsersByBirthday;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUserProducts;

public class GetUserProductsQueryHandler : IQueryHandler<GetUserProductsQuery, IEnumerable<UserProductsResponse>>
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;

    public GetUserProductsQueryHandler(
        IPurchaseRepository purchaseRepository, 
        IProductRepository productRepository)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<UserProductsResponse>>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
    {
        var userPurchases = await _purchaseRepository.GetPurchasesByUserId(request.userId, cancellationToken);

        var purchaseItems = userPurchases
            .SelectMany(p => p.PurchaseItems)
            .ToList();

        var productIds = purchaseItems.Select(pi => pi.ProductId).Distinct().ToList();
       
        var products = await _productRepository.GetByIdAsync(productIds, cancellationToken);

        IEnumerable<UserProductsResponse> categoryCounts = purchaseItems
            .GroupBy(pi => products.First(p => p.Id == pi.ProductId).Category)
            .Select(group => new UserProductsResponse(
                group.Key.Value,
                group.Sum(item => item.Quantity.Value)
            ))
            .ToList();

        return Result.Success(categoryCounts);
    }
}
