﻿using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public class GetUsersByBirthdayQueryHandler : IQueryHandler<GetUsersByBirthdayQuery, IEnumerable<GetUsersByBirthdayResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersByBirthdayQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<GetUsersByBirthdayResponse>>> Handle(GetUsersByBirthdayQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersByBirthday(cancellationToken);

        var usersResponse = users.Select(u => new GetUsersByBirthdayResponse(
            u.Id.Value, 
            u.FullName.FirstName, 
            u.FullName.LastName))
            .ToList();

        return usersResponse;
    }
}
