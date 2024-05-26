using DataAccess.EFCore.Repositories;
using Domain.Interfaces;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;

namespace MedicalAppointmentBooking.WebAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedicalAppointmentBookingDbContext _context;
        public ISpecializationRepository Specializations { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public ICustomerRepository Customers { get; private set; }

        public IPatientRecordRepository PatientRecords { get; set; }

        public UnitOfWork(MedicalAppointmentBookingDbContext context)
        {
            _context = context;
            Specializations = new SpecializationRepository(_context);
            Doctors = new DoctorRepository(_context);
            Customers = new CustomerRepository(_context);
            PatientRecords = new PatientRecordRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
