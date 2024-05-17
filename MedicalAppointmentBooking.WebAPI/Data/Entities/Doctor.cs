using MedicalAppointmentBooking.WebAPI.Models;

namespace MedicalAppointmentBooking.WebAPI.Models.Entities
{
    public class Doctor : BaseEntity
    {
        public int UserId { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }

        public User User { get; set; } = new User();
        public List<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    }
}
