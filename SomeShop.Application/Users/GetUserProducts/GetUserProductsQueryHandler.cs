using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Application.Users.GetUsersByBirthday;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUserProducts;

//public class GetUserProductsQueryHandler : IQueryHandler<GetUserProductsQuery, IReadOnlyList<UserProductsResponse>>
//{
//    private readonly IPurchaseRepository _purchaseRepository;
//    private readonly IProductRepository _productRepository;

//    public GetUserProductsQueryHandler(IPurchaseRepository purchaseRepository, IProductRepository productRepository)
//    {
//        _purchaseRepository = purchaseRepository;
//        _productRepository = productRepository;
//    }

//    public async Task<Result<IReadOnlyList<UserProductsResponse>>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
//    {
//        var purchases = await _purchaseRepository.GetPurchasesByUserId(request.userId, cancellationToken);

//        if (purchases == null || !purchases.Any())
//        {
//            return Result.Failure<IReadOnlyList<UserProductsResponse>>(PurchaseErrors.NotFountPurchases);
//        }

//        var categoryCounts = purchases!
//            .SelectMany(purchase => purchase.Items) 
//            .GroupBy(item => item.ProductId)  
//            .Select(group => new
//            {
//                ProductId = group.Key,
//                TotalQuantity = group.Sum(item => item.Quantity)
//            })
//            .ToList();

//        var categoryCountResponses = new List<UserProductsResponse>();
//        foreach (var categoryCount in categoryCounts)
//        {
//            var product = await _productRepository.GetByIdAsync(categoryCount.ProductId);

//            if (product != null)
//            {
//                categoryCountResponses.Add(new UserProductsResponse(
//                    category: product.Category.Value,
//                    quantity: categoryCount.TotalQuantity
//                ));
//            }
//        }

//        return categoryCountResponses;
//    }
//}
