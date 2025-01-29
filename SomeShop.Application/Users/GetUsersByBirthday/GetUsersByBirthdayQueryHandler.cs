using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public class GetUsersByBirthdayQueryHandler : IQueryHandler<GetUsersByBirthdayQuery, IReadOnlyCollection<GetUsersByBirthdayResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersByBirthdayQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetUsersByBirthdayResponse>>> Handle(GetUsersByBirthdayQuery request, CancellationToken cancellationToken)
    {
        DateTime queryDate = DateTime.UtcNow;

        var users = await _userRepository.GetUsersByBirthday(cancellationToken);

        if (users == null || !users.Any())
        {
            return Result.Failure<IReadOnlyCollection<GetUsersByBirthdayResponse>>(new Error("Firstname.Empty", "First name cannot be empty."));
        }

        var usersResponse = users.Select(u => new GetUsersByBirthdayResponse(
            u.Id.Value, 
            u.FullName.FirstName, 
            u.FullName.LastName))
            .ToList();

        return usersResponse;
    }
}
