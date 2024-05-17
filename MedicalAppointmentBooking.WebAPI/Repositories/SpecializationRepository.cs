using AutoMapper;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBooking.WebAPI.Repositories
{
    public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationRepository
    {
        private readonly MedicalAppointmentBookingDbContext _context;

        public SpecializationRepository(MedicalAppointmentBookingDbContext context) : base(context)
        {
            _context = context;

        }

        

        public async Task<bool> SpecializationExists(int specializationId)
        {
            return await _context.Specializations.AnyAsync(s => s.Id == specializationId);
        }

        
    }
}
