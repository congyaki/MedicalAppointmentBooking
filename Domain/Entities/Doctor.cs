using MedicalAppointmentBooking.WebAPI.Models;

namespace MedicalAppointmentBooking.WebAPI.Models.Entities
{
    public class Doctor : BaseEntity
    {
        public string UserId { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }
        public List<DoctorSpecialization> DoctorSpecializations { get; set; }
    }
}
