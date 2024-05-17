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

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalAppointmentBookingDbContext _context;
        private readonly IDoctorRepository _doctorRepo;

        public DoctorsController(MedicalAppointmentBookingDbContext context, IDoctorRepository doctorRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorVM>>> GetDoctors()
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            return Ok (/*await _doctorRepo.GetAllDoctors()*/);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(AddDoctorVM _doctor)
        {
          if (_context.Doctors == null)
          {
              return Problem("Entity set 'MedicalAppointmentBookingDbContext.Doctors'  is null.");
          }

            var newDoctor = new Doctor()
            {
                UserId = _doctor.UserId,
                Avatar = _doctor.Avatar,
                Title = _doctor.Title,
                Experience = _doctor.Experience,
                /*DoctorSpecializations = new List<DoctorSpecialization>()*/
            };
            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            if (_doctor.SpecializationIds != null)
            {
                foreach (var specializationId in _doctor.SpecializationIds)
                {
                    newDoctor.DoctorSpecializations.Add(new DoctorSpecialization
                    {
                        DoctorId = newDoctor.Id,
                        SpecializationId = specializationId
                    });

                    await _context.SaveChangesAsync();

                }
            }
            return Ok(newDoctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
