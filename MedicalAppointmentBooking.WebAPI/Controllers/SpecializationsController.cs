using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using AutoMapper;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SpecializationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Specializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationVM>>> GetSpecializations()
        {
            var specializations = _unitOfWork.Specializations.GetAll();

          if (specializations == null)
          {
              return NotFound();
          }
            return Ok(_mapper.Map<List<SpecializationVM>>(specializations));
        }

        // GET: api/Specializations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationVM>> GetSpecialization(int id)
        {
            if(!await _unitOfWork.Specializations.SpecializationExists(id))
            {
                return NotFound();
            }
            var specialization = _unitOfWork.Specializations.GetById(id);

            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpecializationVM>(specialization));
        }

        // PUT: api/Specializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, SpecializationVM model)
        {

            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            if(!await _unitOfWork.Specializations.SpecializationExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updateSpecialization = _mapper.Map<Specialization>(model);
            _unitOfWork.Specializations.Update(updateSpecialization);
            _unitOfWork.Complete();

            return Ok(updateSpecialization.Id);
        }

        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialization>> PostSpecialization(SpecializationVM model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSpecialization = _mapper.Map<Specialization>(model);
            _unitOfWork.Specializations.Add(newSpecialization);
            _unitOfWork.Complete();

            return CreatedAtAction("GetSpecialization", new { id = newSpecialization.Id }, newSpecialization.Id);
        }

        // DELETE: api/Specializations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {

            if (!await _unitOfWork.Specializations.SpecializationExists(id))
            {
                return NotFound();
            }
            var deleteSpecialization = _unitOfWork.Specializations.GetById(id);
            _unitOfWork.Specializations.Remove(deleteSpecialization);
            _unitOfWork.Complete();
            return NoContent();
        }

    }
}
