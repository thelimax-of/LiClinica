using LiClinica.Application.Patients.Dtos;
using LiClinica.Application.Patients.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/patients")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("me")]
        public async Task<IActionResult> CreateProfile([FromBody] CreatePatientRequestDto dto, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var id))
                return Unauthorized();

            var patient = await _patientService.CreateProfileAsync(id, dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, patient);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var id))
                return Unauthorized();

            var profile = await _patientService.GetMyProfileAsync(id, cancellationToken);

            return Ok(profile);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdatePatientRequestDto dto, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var id))
                return Unauthorized();

            var profile = await _patientService.UpdateMyProfileAsync(id, dto, cancellationToken);

            return Ok(profile);
        }
    }
}
