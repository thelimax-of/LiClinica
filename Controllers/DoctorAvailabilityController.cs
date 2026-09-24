using LiClinica.Application.DoctorAvailabilities.Dtos;
using LiClinica.Application.DoctorAvailabilities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/doctors/{doctorId}/availabilities")]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilitiesService _service;
        public DoctorAvailabilityController(IDoctorAvailabilitiesService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid doctorId, [FromBody] CreateDoctorAvailabilityRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _service.CreateAsync(doctorId, dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByDoctorId([FromRoute] Guid doctorId, CancellationToken cancellationToken)
        {
            var response = await _service.GetByDoctorIdAsync(doctorId, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{availabilityId}")]
        public async Task<IActionResult> Update([FromRoute] Guid doctorId, [FromRoute] Guid availabilityId, [FromBody] UpdateDoctorAvailabilityRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _service.UpdateAsync(doctorId, availabilityId, dto, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{availabilityId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid doctorId, [FromRoute] Guid availabilityId, CancellationToken cancellationToken)
        {
            await _service.RemoveAsync(doctorId, availabilityId, cancellationToken);

            return NoContent();
        }
    }
}
