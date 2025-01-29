using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;
using SomeShop.Domain.Purchases;
using MediatR;

namespace SomeShop.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserId>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPurchaseRepository purchaseRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UserId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.firstName, request.lastName);
         
        if (fullName.IsFailure)
        {
            return Result.Failure<UserId>(fullName.Error);
        }

        var newUser = User.Create(fullName.Value, request.dateOfBirth);

        if (newUser.IsFailure)
        {
            return Result.Failure<UserId>(newUser.Error);
        }

        _userRepository.Add(newUser.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //return newUser.Value.Id;

        return Result.Success(newUser.Value.Id);
    }
}
