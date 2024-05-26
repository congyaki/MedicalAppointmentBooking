using Domain.Entities;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPatientRecordRepository : IGenericRepository<PatientRecord>
    {
    }
}
