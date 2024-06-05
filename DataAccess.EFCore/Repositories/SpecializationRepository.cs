using Domain.ViewModels;
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

        public async Task<List<DoctorBasicVM>> GetDoctorInSpecialization(int specializationId)
        {
            var response = await _context.DoctorSpecializations
                .Where(ds => ds.SpecializationId == specializationId)
                .Include(ds => ds.Doctor)
                    .ThenInclude(d => d.User) // Bao gồm đối tượng User để ánh xạ FullName
                .ToListAsync();
            var specialization = await _context.Specializations.FirstOrDefaultAsync(s => s.Id == specializationId);
            // Chuyển đổi danh sách các bác sĩ thành danh sách DoctorBasicVM
            var doctorsBasicVM = response.Select(ds => new DoctorBasicVM
            {
                Id = ds.Doctor.Id,
                Experience = ds.Doctor.Experience,
                Avatar = ds.Doctor.Avatar,
                FullName = $"{ds.Doctor.User.FirstName} {ds.Doctor.User.LastName}",
                SpecializationName = specialization.Name,
            }).ToList();

            return doctorsBasicVM;
        }

        public async Task<bool> SpecializationExists(int specializationId)
        {
            return await _context.Specializations.AnyAsync(s => s.Id == specializationId);
        }

        
    }
}
