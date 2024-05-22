namespace MedicalAppointmentBooking.WebAPI.Models.Entities
{
    public class DoctorSpecialization
    {
        public int SpecializationId { get; set; }
        public int DoctorId { get; set; }
        public Specialization Specialization { get; set; } = new Specialization();
        public Doctor Doctor { get; set; } = new Doctor();
    }
}
