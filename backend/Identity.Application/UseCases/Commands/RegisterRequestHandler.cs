using Azure.Core;
using IdentityContext.Application.Errors;
using IdentityContext.Contracts.Commands.Register;
using IdentityContext.DL.Entities.ApplicationUser;
using IdentityContext.DL.Extensions;
using IdentityContext.DL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Extensions;
using Shared.Core.Models;
using UserContext.Contract.Commands.CreateUser;

namespace IdentityContext.Application.UseCases.Commands
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<Guid?>>
    {
        private UserManager<ApplicationUser> _userManager;
        private IIdentityChecker _checker;
        private IMediator _mediator;

        public RegisterRequestHandler(UserManager<ApplicationUser> userManager, IIdentityChecker checker, IMediator mediator)
        {
            _userManager = userManager;
            _checker = checker;
            _mediator = mediator;
        }

        public async Task<Result<Guid?>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _checker.IsEmailTaken(request.Email) || await _checker.IsLoginTaken(request.Login))
            {
                return Result<Guid?>.Failure(ApplicationUserErrors.UNIQUE_IDENTITY);
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Login,
                AvatarId = request.AvatarId
            };

            var registrationResult = await Register(user, request.Password);

            if (registrationResult.IsFailure)
            {
                return registrationResult.ToFailure<Guid?>();
            }

            var creationResult = await CreateUser(request);

            if (creationResult.IsFailure)
            {
                await _userManager.DeleteAsync(user);
                return creationResult.ToFailure<Guid?>();
            }
            else
            {
                user.UserId = creationResult.Model.UserId;
                await _userManager.UpdateAsync(user);
            }

            return user.Id;
        }

        private async Task<Result> Register(ApplicationUser user, string password)
        {
            var registrationResult = await _userManager.CreateAsync(user, password);

            return registrationResult.ToResult();
        }

        private Task<Result<CreateUserResponse>> CreateUser(RegisterRequest request)
        {
            var userCreationRequest = new CreateUserRequest(
                request.FirstName, 
                request.LastName, 
                request.BirthDate);

            return _mediator.Send(userCreationRequest);
        }
    }
}
