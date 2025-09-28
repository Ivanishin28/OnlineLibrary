using IdentityContext.Application.Errors;
using IdentityContext.Application.Interfaces;
using IdentityContext.Contracts.Commands.Login;
using IdentityContext.Contracts.Dtos;
using IdentityContext.DL.Entities.ApplicationUser;
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
        private ITokenBuilder _tokenBuilder;

        public LoginRequestHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenBuilder tokenBuilder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenBuilder = tokenBuilder;
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

            return BuildResponseFrom(user);
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

        private ApplicationUserLoginDto BuildResponseFrom(ApplicationUser user)
        {
            var token = _tokenBuilder.BuildFor(user);

            var tokenDto = new TokenDto(token.Value);

            return new ApplicationUserLoginDto(
                user.Email!,
                user.UserName!,
                user.Id,
                user.UserId!.Value,
                tokenDto);
        }
    }
}
