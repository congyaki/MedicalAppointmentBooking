namespace MedicalAppointmentBooking.WebAPI.Entities
{
    public class DoctorSpecialization
    {
        public int SpecializationID { get; set; }
        public int DoctorID { get; set; }
        public Specialization Specialization { get; set; }
        public Doctor Doctor { get; set; }
    }
}
