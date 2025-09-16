using IdentityContext.Application.Errors;
using IdentityContext.Contracts.Commands.Login;
using IdentityContext.DL.Entities.ApplicationUser;
using IdentityContext.DL.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Extensions;
using Shared.Core.Models;

namespace IdentityContext.Application.UseCases.Commands
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, Result<ApplicationUserLoginDto>>
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public LoginRequestHandler(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<ApplicationUserLoginDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await GetBy(request.Login);

            if (user is null)
            {
                return Result<ApplicationUserLoginDto>.Failure(LoginErrors.LOGIN_NOT_FOUND);
            }

            var loginResult = await SignIn(user, request.Password);

            if (loginResult.IsFailure)
            {
                return loginResult.ToFailure<ApplicationUserLoginDto>();
            }

            return new ApplicationUserLoginDto(
                user.Email!, 
                user.UserName!, 
                user.Id);
        }

        private Task<ApplicationUser?> GetBy(string login)
        {
            return _userManager.FindByNameAsync(login);
        }

        private async Task<Result> SignIn(ApplicationUser user, string password)
        {
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded ?
                Result.Success() :
                Result.Failure(LoginErrors.NOT_ALLOWED);
        }
    }
}
