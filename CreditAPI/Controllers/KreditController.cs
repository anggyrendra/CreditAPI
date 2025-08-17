using CreditAPI.DTOs;
using CreditAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CreditAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KreditController : ControllerBase
    {
        private readonly IKreditService _service;

        public KreditController(IKreditService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KreditCreateDto dto)
        {
            try
            {
                var result = await _service.CreateKredit(dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetKreditById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllKredits();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] KreditUpdateDto dto)
        {
            try
            {
                var result = await _service.UpdateKredit(id, dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteKredit(id);
                return Ok(new { message = "Data berhasil dihapus" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("calculate")]
        public IActionResult CalculateAngsuran([FromBody] KreditCreateDto dto)
        {
            try
            {
                var angsuran = _service.CalculateAngsuran(dto.Plafon, dto.Bunga, dto.Tenor);
                return Ok(new { angsuran });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
