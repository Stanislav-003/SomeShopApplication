using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.CreatePurchase;

public record CreatePurchaseCommandHandler : ICommandHandler<CreatePurchaseCommand, Guid>
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseCommandHandler(
        IPurchaseRepository purchaseRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
        if (user == null)
            return Result.Failure<Guid>(UserErrors.NotFound);

        var newPurchase = Purchase.Create(request.number, request.totalPrice, request.userId);

        foreach (var item in request.items)
        {
            var result = newPurchase.AddItem(item.productId, item.quantity, item.price);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);
        }

        _purchaseRepository.Add(newPurchase);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newPurchase.Id;
    }
}
