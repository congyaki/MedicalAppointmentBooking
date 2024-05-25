using MedicalAppointmentBooking.WebAPI.Models;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
