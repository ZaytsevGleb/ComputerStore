using ComputerStore.Services.Auth.BusinessLogic.Errors;
using ComputerStore.Services.IdentityServer.WebApi.Common;
using ComputerStore.Services.IdentityServer.WebApi.Dtos;
using ComputerStore.Services.IdentityServer.WebApi.Entities;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ComputerStore.Services.IdentityServer.WebApi.Controllers
{
    [ApiController]
    [Route(ApiConstants.Auth)]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            UserManager<User> userManager,
        SignInManager<User> signInManager,
        IIdentityServerInteractionService interactionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interactionService = interactionService;
        }

        [HttpPost(ApiConstants.Register)]
        public async Task<ActionResult> Register(RegisterDto dto, CancellationToken ct)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                NormalizedUserName = dto.Name,
                UserName = dto.Name
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = JsonConvert.SerializeObject(result.Errors);
                throw new BadRequestException($"Some errors during creation of user: {errors}");
            }

            return Ok(User);
        }

        [HttpPost(ApiConstants.Login)]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                return new LoginResponseDto()
                {
                    Succeeded = false,
                    FailureReason = FailureReason.UserNotFound
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new LoginResponseDto()
                {
                    Succeeded = false,
                    FailureReason = FailureReason.WrongPassword
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);

            if (!signInResult.Succeeded)
            {
                return new LoginResponseDto()
                {
                    Succeeded = false,
                    FailureReason = FailureReason.UnknownReason
                };
            }

            return new LoginResponseDto()
            {
                // TODO: add token
                Succeeded = true,
                AccessToken = "token"
            };
        }

        [Authorize]
        [HttpPost(ApiConstants.logout)]
        public async Task<ActionResult> Logout(string logoutId,CancellationToken ct)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Ok(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
