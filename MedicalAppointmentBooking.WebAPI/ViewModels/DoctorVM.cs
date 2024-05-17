using MedicalAppointmentBooking.WebAPI.Models.Entities;

namespace MedicalAppointmentBooking.WebAPI.ViewModels
{
    public class DoctorVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public List<Specialization> Specializations { get; set; }

    }
}
