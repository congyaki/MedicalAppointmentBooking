using MedicalAppointmentBooking.WebAPI.Models;

namespace MedicalAppointmentBooking.WebAPI.Entities
{
    public class Doctor : BaseEntity
    {
        public int UserID { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }

        public User User { get; set; }
        public List<DoctorSpecialization> DoctorSpecializations { get; set; }
    }
}
