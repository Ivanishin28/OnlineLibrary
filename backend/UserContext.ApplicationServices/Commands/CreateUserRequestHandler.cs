using MediatR;
using Shared.Core.Models;
using UserContext.Contract.Commands.CreateUser;
using UserContext.DL.Repositories;
using UserContext.Domain.Entities;

namespace UserContext.UseCases.Commands
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        private IUserRepository _userRepository;

        public CreateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userResult = User.Create(request.FirstName, request.LastName, request.BirthDate);

            if(userResult.IsFailure)
            {
                return Result<CreateUserResponse>.Failure(userResult.Errors.ToArray());
            }

            var user = userResult.Model;

            await _userRepository.Add(user);

            return new CreateUserResponse(user.Id);
        }
    }
}
