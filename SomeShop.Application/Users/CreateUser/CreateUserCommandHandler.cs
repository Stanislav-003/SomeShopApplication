using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;
using SomeShop.Domain.Purchases;

namespace SomeShop.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository, 
        IPurchaseRepository purchaseRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _purchaseRepository = purchaseRepository;
        _unitOfWork = unitOfWork;
    }

     
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = User.Create(request.firstName, request.lastName, request.dateOfBirth);

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
