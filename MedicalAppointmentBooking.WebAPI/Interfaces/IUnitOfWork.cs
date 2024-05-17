namespace MedicalAppointmentBooking.WebAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecializationRepository Specializations { get; }
        IDoctorRepository Doctors { get; }
        int Complete();
    }
}
