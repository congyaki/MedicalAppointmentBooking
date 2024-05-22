using MedicalAppointmentBooking.WebAPI.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalAppointmentBooking.WebAPI.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly MedicalAppointmentBookingDbContext _context;

        public DoctorRepository(MedicalAppointmentBookingDbContext context): base(context)
        {
            _context = context;
        }


        public int AddDoctorWithSpecialization(Doctor doctor, IEnumerable<int> specializationIds)
        {
            Add(doctor);

            foreach (var specializationId in specializationIds)
            {
                doctor.DoctorSpecializations.Add(new DoctorSpecialization
                {
                    DoctorId = doctor.Id,
                    SpecializationId = specializationId
                });

                _context.SaveChangesAsync();
            }
            return doctor.Id;
        }

        public DoctorVM GetDoctorWithSpecialization(int doctorId)
        {
            var doctor = GetById(doctorId);
            var listSpecialization = _context.DoctorSpecializations
                                    .Where(e => e.DoctorId == doctorId)
                                    .Select(e => e.Specialization)
                                    .ToList();
            DoctorVM doctorVM = new DoctorVM
            {
                Id = doctorId,
                Avatar = doctor.Avatar,
                Experience = doctor.Experience,
                Specializations = listSpecialization,
                Title = doctor.Title,
                UserId = doctor.UserId,
            };

            return doctorVM;
            
        }

        public int UpdateDoctorrWithSpecialization(Doctor doctor, IEnumerable<int> specializationIds)
        {
            Update(doctor);

            var existSpecializations = _context.DoctorSpecializations.Where(s => specializationIds.Contains(s.SpecializationId)).ToList();

            _context.DoctorSpecializations.RemoveRange(existSpecializations);

            _context.SaveChangesAsync();


            foreach (var specializationId in specializationIds)
            {
                doctor.DoctorSpecializations.Add(new DoctorSpecialization
                {
                    DoctorId = doctor.Id,
                    SpecializationId = specializationId
                });

                _context.SaveChangesAsync();
            }
            return doctor.Id;
        }
    }
}
