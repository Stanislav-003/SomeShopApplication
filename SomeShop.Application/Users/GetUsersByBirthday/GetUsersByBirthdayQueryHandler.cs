using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public class GetUsersByBirthdayQueryHandler : IQueryHandler<GetUsersByBirthdayQuery, IReadOnlyList<UsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersByBirthdayQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IReadOnlyList<UsersResponse>>> Handle(GetUsersByBirthdayQuery request, CancellationToken cancellationToken)
    {
        DateTime queryDate = request.birthday ?? DateTime.UtcNow;

        var users = await _userRepository.GetUsersByBirthday(queryDate, cancellationToken);

        var usersResponse = users.Select(u => new UsersResponse(u.Id, u.FirstName.Value, u.LastName.Value)).ToList();

        return usersResponse;
    }
}
