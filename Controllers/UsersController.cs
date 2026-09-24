using LiClinica.Application.Auth.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);

            var roles = User.FindAll(ClaimTypes.Role)
                .Select(claim => claim.Value)
                .ToList();

            _ = Guid.TryParse(userIdClaim, out var userId);

            var response = new UsersMeReponseDto
            {
                Id = userId,
                Email = email!,
                Roles = roles
            };

            return Ok(response);
        }
    }
}
