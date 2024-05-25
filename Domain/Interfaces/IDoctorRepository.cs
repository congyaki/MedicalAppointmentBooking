using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;

namespace MedicalAppointmentBooking.WebAPI.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        public DoctorVM GetDoctorWithSpecialization(int doctorId);
        public int AddDoctorWithSpecialization(AddDoctorVM addDoctor);
        public int UpdateDoctorrWithSpecialization(Doctor doctor, IEnumerable<int> specializationIds);

    }
}
