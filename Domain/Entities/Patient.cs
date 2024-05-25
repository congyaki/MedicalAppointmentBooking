﻿using MedicalAppointmentBooking.WebAPI.Models;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public User User { get; set; }
    }
}
