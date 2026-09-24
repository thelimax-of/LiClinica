using FluentValidation;
using LiClinica.Application.Auth.Dtos;
using LiClinica.Application.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UserRegisterRequestDto> _registerValidator;
        private readonly IValidator<UserLoginRequestDto> _loginValidator;

        public AuthController(
            IAuthService authService, 
            IValidator<UserRegisterRequestDto> authValidator, 
            IValidator<UserLoginRequestDto> loginValidator)
        {
            _authService = authService;
            _registerValidator = authValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _registerValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                return BadRequest(result.ToDictionary());

            await _authService.RegisterAsync(dto, cancellationToken);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _loginValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                return BadRequest(result.ToDictionary());

            var response = await _authService.LoginAsync(dto, cancellationToken);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _authService.RefreshAsync(dto, cancellationToken);

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto dto, CancellationToken cancellationToken)
        {
            await _authService.LogoutAsync(dto, cancellationToken);

            return NoContent();
        }

    }
}
