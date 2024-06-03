using Domain.Entities;
using Domain.ViewModels;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPatientRecordRepository : IGenericRepository<PatientRecord>
    {
        public Task<List<PatientRecord>> GetPatientRecordOfCustomer(int customerId);
    }
}
