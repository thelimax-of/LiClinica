using LiClinica.Application.Specialties.Dtos;
using LiClinica.Application.Specialties.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiClinica.Api.Controllers
{
    [ApiController]
    [Route("api/v1/specialties")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpecialtyRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _specialtyService.CreateAsync(dto, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _specialtyService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _specialtyService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSpecialtyRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _specialtyService.UpdateAsync(id, dto, cancellationToken);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateSpecialtyStatusRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _specialtyService.UpdateStatusAsync(id, dto.Status, cancellationToken);

            return Ok(response);
        }
    }
}
