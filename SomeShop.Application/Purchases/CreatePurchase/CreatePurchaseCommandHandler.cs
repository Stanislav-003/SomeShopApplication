using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Application.Users.CreateUser;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Purchases.CreatePurchase;

public class CreatePurchaseCommandHandler : ICommandHandler<CreatePurchaseCommand, PurchaseId>
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseCommandHandler(
        IPurchaseRepository purchaseRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PurchaseId>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Items.Select(item => new ProductId(item.ProductId)).ToList();
        var products = await _productRepository.GetByIdAsync(productIds, cancellationToken);

        if (products.Count() != request.Items.Count)
        {
            return Result.Failure<PurchaseId>(new Error("Products.NotFound", "Some products not found"));
        }

        var purchaseNumber = new Number(Guid.NewGuid().ToString());
        var totalPrice = new TotalPrice(
            request.Items.Sum(item =>
            {
                var product = products.First(p => p.Id.Value == item.ProductId);
                return product.Price.Value * item.Quantity;
            }));

        var purchaseResult = Purchase.Create(purchaseNumber, totalPrice, new UserId(request.UserId));

        if (purchaseResult.IsFailure)
        {
            return Result.Failure<PurchaseId>(purchaseResult.Error);
        }

        var purchase = purchaseResult.Value;

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id.Value == item.ProductId);
            var purchaseItemResult = PurchaseItem.Create(new Quantity(item.Quantity), new PricePerUnit(product.Price.Value), purchase.Id, product.Id);

            if (purchaseItemResult.IsFailure)
            {
                return Result.Failure<PurchaseId>(purchaseItemResult.Error);
            }

            purchase.AddPurchaseItem(purchaseItemResult.Value);
        }

        _purchaseRepository.Add(purchase);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(purchase.Id);
    }
}
