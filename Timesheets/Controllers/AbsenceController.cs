using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DAL.Entitys;
using Timesheets.DAL.Interfaces;
using Timesheets.DTO;

namespace Timesheets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceRepository _repository;
        private readonly IMapper _mapper;

        public AbsenceController(IAbsenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<AbsenceController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAllAsync());
        }

        // GET api/<AbsenceController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repository.GetByIdAsync(id));
        }

        // POST api/<AbsenceController>
        [HttpPost]
        public IActionResult Post([FromBody] AbsenceDTO absence)
        {
            _repository.CreateAsync(_mapper.Map<AbsenceEntity>(absence));
            return Ok();
        }

        // PUT api/<AbsenceController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] AbsenceDTO absence)
        {
            _repository.UpdateAsync(_mapper.Map<AbsenceEntity>(absence));
            return Ok();
        }

        // DELETE api/<AbsenceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
