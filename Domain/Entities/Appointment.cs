using MedicalAppointmentBooking.WebAPI.Models;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientRecordId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public PatientRecord PatientRecord { get; set; }
        public Doctor Doctor { get; set; }
    }
}
