using MediatR;
using Microsoft.EntityFrameworkCore;
using UserContext.Contract.Dtos;
using UserContext.Contract.Queries;
using UserContext.DL.SqlServer;

namespace UserContext.ApplicationServices.Queries
{
    public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, UserDto?>
    {
        private UserDbContext _db;

        public GetUserByUserIdQueryHandler(UserDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto?> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _db
                .Users
                .Where(x => x.Id == request.UserId)
                .Select(x => new UserDto(x.Id, x.FirstName, x.LastName, x.BirthDate))
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}

