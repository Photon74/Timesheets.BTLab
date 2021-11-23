using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DTO;
using Timesheets.Services;

namespace Timesheets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _service;

        public AbsenceController(IAbsenceService service)
        {
            _service = service;
        }

        // GET: api/<AbsenceController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET api/<AbsenceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        // POST api/<AbsenceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AbsenceDTO absence)
        {
            await _service.CreateAsync(absence);
            return Ok();
        }

        // PUT api/<AbsenceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AbsenceDTO absence)
        {
            await _service.UpdateAsync(id, absence);
            return Ok();
        }

        // DELETE api/<AbsenceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
