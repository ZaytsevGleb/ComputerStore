using AutoMapper;
using ComputerStore.Services.Auth.Api.Constants;
using ComputerStore.Services.Auth.Api.Dtos;
using ComputerStore.Services.Auth.BusinessLogic.Abstractions;
using ComputerStore.Services.Auth.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Services.Auth.Api.Controllers;

[ApiController]
[Route(ControllerConstants.Auth)]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService,
        IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost(ControllerConstants.Register)]
    public async Task<ActionResult> Register(RegisterDto dto, CancellationToken ct)
    {
        var result = await _authService.Register(_mapper.Map<RegisterModel>(dto), ct);
        return Ok(result);
    }

    [HttpPost(ControllerConstants.Login)]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto, CancellationToken ct)
    {
        var result = await _authService.Login(_mapper.Map<LoginModel>(dto), ct);
        return Ok(_mapper.Map<LoginResponseDto>(result));
    }
    [Authorize]
    [HttpPost(ControllerConstants.Logout)]
    public async Task<ActionResult> Logout(CancellationToken ct)
    {
        await _authService.Logout(ct);
        return Ok();
    }
}