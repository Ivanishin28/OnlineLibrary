using Azure.Core;
using IdentityContext.Contracts.Commands.Register;
using IdentityContext.DL.Entities.ApplicationUser;
using IdentityContext.DL.Extensions;
using IdentityContext.DL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Extensions;
using Shared.Core.Models;

namespace IdentityContext.Application.UseCases.Commands
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<Guid>>
    {
        private UserManager<ApplicationUser> _userManager;
        private IIdentityChecker _checker;

        public RegisterRequestHandler(UserManager<ApplicationUser> userManager, IIdentityChecker checker)
        {
            _userManager = userManager;
            _checker = checker;
        }

        public async Task<Result<Guid>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _checker.IsEmailTaken(request.Email) || await _checker.IsLoginTaken(request.Login))
            {
                return Result<Guid>.Failure(new Error("", ""));
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Login
            };

            var registrationResult = await Register(user, request.Password);

            if (registrationResult.IsFailure)
            {
                return registrationResult.ToFailure<Guid>();
            }

            return user.Id;
        }

        private async Task<Result> Register(ApplicationUser user, string password)
        {
            var registrationResult = await _userManager.CreateAsync(user, password);

            return registrationResult.ToResult();
        }
    }
}
