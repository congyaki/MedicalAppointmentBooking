using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentBooking.WebAPI.EF;
using MedicalAppointmentBooking.WebAPI.Entities;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecializationsController : ControllerBase
    {
        private readonly MedicalAppointmentBookingDbContext _context;

        public DoctorSpecializationsController(MedicalAppointmentBookingDbContext context)
        {
            _context = context;
        }

        // GET: api/DoctorSpecializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorSpecialization>>> GetDoctorSpecializations()
        {
          if (_context.DoctorSpecializations == null)
          {
              return NotFound();
          }
            return await _context.DoctorSpecializations.ToListAsync();
        }

        // GET: api/DoctorSpecializations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorSpecialization>> GetDoctorSpecialization(int id)
        {
          if (_context.DoctorSpecializations == null)
          {
              return NotFound();
          }
            var doctorSpecialization = await _context.DoctorSpecializations.FindAsync(id);

            if (doctorSpecialization == null)
            {
                return NotFound();
            }

            return doctorSpecialization;
        }

        // PUT: api/DoctorSpecializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctorSpecialization(int id, DoctorSpecialization doctorSpecialization)
        {
            if (id != doctorSpecialization.DoctorID)
            {
                return BadRequest();
            }

            _context.Entry(doctorSpecialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorSpecializationExists(id))
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

        // POST: api/DoctorSpecializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorSpecialization>> PostDoctorSpecialization(DoctorSpecialization doctorSpecialization)
        {
          if (_context.DoctorSpecializations == null)
          {
              return Problem("Entity set 'MedicalAppointmentBookingDbContext.DoctorSpecializations'  is null.");
          }
            _context.DoctorSpecializations.Add(doctorSpecialization);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctorSpecializationExists(doctorSpecialization.DoctorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctorSpecialization", new { id = doctorSpecialization.DoctorID }, doctorSpecialization);
        }

        // DELETE: api/DoctorSpecializations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorSpecialization(int id)
        {
            if (_context.DoctorSpecializations == null)
            {
                return NotFound();
            }
            var doctorSpecialization = await _context.DoctorSpecializations.FindAsync(id);
            if (doctorSpecialization == null)
            {
                return NotFound();
            }

            _context.DoctorSpecializations.Remove(doctorSpecialization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorSpecializationExists(int id)
        {
            return (_context.DoctorSpecializations?.Any(e => e.DoctorID == id)).GetValueOrDefault();
        }
    }
}
