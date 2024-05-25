using Microsoft.AspNetCore.Http;

namespace MedicalAppointmentBooking.WebAPI.ViewModels
{
    public class AddDoctorVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<int> SpecializationIds { get; set; }
    }
}
