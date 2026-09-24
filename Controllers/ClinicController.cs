using LiClinica.Application.Clinics.Dtos;
using LiClinica.Application.Clinics.Services;
using LiClinica.Application.Patients.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/clinics")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClinicRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _clinicService.CreateAsync(dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var clinics = await _clinicService.GetAllAsync(cancellationToken);

            return Ok(clinics);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var clinic = await _clinicService.GetByIdAsync(id, cancellationToken);

            return Ok(clinic);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClinicRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _clinicService.UpdateAsync(id, dto, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateStatusClinicRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _clinicService.UpdateStatusAsync(id, dto.Status, cancellationToken);

            return Ok(response);
        }
    }
}
