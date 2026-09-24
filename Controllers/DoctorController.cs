using LiClinica.Application.Doctors.Dtos;
using LiClinica.Application.Doctors.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _doctorService.CreateAsync(dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _doctorService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id,  CancellationToken cancellationToken)
        {
            var response = await _doctorService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDoctorRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _doctorService.UpdateAsync(id, dto, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateDoctorStatusRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _doctorService.UpdateStatusAsync(id, dto.Status, cancellationToken);

            return Ok(response);
        }
    }
}
