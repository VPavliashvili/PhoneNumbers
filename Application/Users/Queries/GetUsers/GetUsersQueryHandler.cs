using MediatR;

namespace Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    public Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<UserResponse>>(new List<UserResponse>()
        {
            new("idk", "asdas", "usrname"),
            new("idk2", "asdas2", "usrname2")
        });
    }
}