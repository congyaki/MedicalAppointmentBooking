using MedicalAppointmentBooking.WebAPI.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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


        public int AddDoctorWithSpecialization(AddDoctorVM addDoctor)
        {
            Doctor doctor = new Doctor()
            {
                PhoneNumber = addDoctor.PhoneNumber,
                DateOfBirth = addDoctor.DateOfBirth,
                Address = addDoctor.Address,
                Experience = addDoctor.Experience,
                Title = addDoctor.Title,
                UserId = addDoctor.UserId,
            };

            // tải lên các file ảnh khác
            if (addDoctor.Avatar != null && addDoctor.Avatar.Length > 0)
            {

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(addDoctor.Avatar.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    return 0;
                }
                // Get the filename and extension
                var fileName = Path.GetFileNameWithoutExtension(addDoctor.Avatar.FileName);

                // Create a unique filename
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Create a path for the image file
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductImage", uniqueFileName);

                // Save the image file to the server
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    addDoctor.Avatar.CopyToAsync(stream);
                }

                // Set the product avatar property to the filename
                doctor.Avatar = uniqueFileName;
            }
            Add(doctor);
            _context.SaveChangesAsync();

            foreach (var specializationId in addDoctor.SpecializationIds)
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
                Address = doctor.Address,
                DateOfBirth = doctor.DateOfBirth,
                PhoneNumber = doctor.PhoneNumber,
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
