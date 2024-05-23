﻿
using Microsoft.AspNetCore.Identity;

namespace MedicalAppointmentBooking.WebAPI.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /*public Doctor Doctor { get; set; } = new Doctor();*/

    }
}
