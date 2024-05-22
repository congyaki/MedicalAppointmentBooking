using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;

namespace MedicalAppointmentBooking.WebAPI.Interfaces
{
    public interface ISpecializationRepository : IGenericRepository<Specialization>
    {
        
        public Task<bool> SpecializationExists(int specializationId);
    }
}
