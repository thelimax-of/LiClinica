using LiClinica.Application.Appointments.Dtos;
using LiClinica.Application.Appointments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentRequestDto dto, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var response = await _appointmentService.CreateAsync(userId, dto, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAppointments(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var response = await _appointmentService.GetMyAppointmentsAsync(userId, cancellationToken);
            return Ok(response);
        }

        [HttpPatch("{appointmentId}/cancel")]
        public async Task<IActionResult> Cancel(Guid appointmentId, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var response = await _appointmentService.CancelAsync(userId, appointmentId, cancellationToken);
            return Ok(response);
        }
    }
}
