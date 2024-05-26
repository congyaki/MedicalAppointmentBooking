using MedicalAppointmentBooking.WebAPI.Models;

namespace MedicalAppointmentBooking.WebAPI.Models.Entities
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DoctorSpecialization> DoctorSpecializations { get; set; }
    }
}
