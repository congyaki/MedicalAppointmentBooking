using Domain.Interfaces;

namespace MedicalAppointmentBooking.WebAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecializationRepository Specializations { get; }
        IDoctorRepository Doctors { get; }
        ICustomerRepository Customers { get; }
        IPatientRecordRepository PatientRecords { get; }
        IAppointmentRepository Appointments { get;}
        int Complete();
    }
}
