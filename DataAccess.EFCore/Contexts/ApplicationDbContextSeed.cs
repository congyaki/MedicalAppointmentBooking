using Domain.Constants;
using Domain.Entities;
using Domain.ViewModels;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Doctor.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            //Seed Default User
            var defaultUser = new User { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MedicalAppointmentBookingDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MedicalAppointmentBookingDbContext>>()))
            {
                // Look for any existing patient records.
                if (!context.PatientRecords.Any())
                {
                    context.PatientRecords.AddRange(
                        new PatientRecord(
                            customerId: 2,
                            firstName: "John",
                            lastName: "Doe",
                            dateOfBirth: new DateTime(1980, 5, 20),
                            gender: "Male",
                            address: "123 Main St, Springfield",
                            phoneNumber: "123-456-7890",
                            email: "john.doe@example.com"
                        ),
                        new PatientRecord(
                            customerId: 2,
                            firstName: "Jane",
                            lastName: "Smith",
                            dateOfBirth: new DateTime(1992, 7, 15),
                            gender: "Female",
                            address: "456 Elm St, Springfield",
                            phoneNumber: "987-654-3210",
                            email: "jane.smith@example.com"
                        )
                    );

                    context.SaveChanges();
                }

                if (!context.Doctors.Any())
                {
                    context.Doctors.AddRange(
                        new Doctor
                        {
                            UserId = "2aa56dcb-8ebe-4b0e-87b0-d8e9ea569414",
                            Experience = 10,
                            Title = "Dr.",
                            Avatar = "avatar1.png",
                            PhoneNumber = "111-222-3333",
                            Address = "789 Maple St, Springfield",
                            DateOfBirth = new DateTime(1975, 3, 10)
                        }
                    );

                    context.SaveChanges();
                }

                if (!context.Appointments.Any())
                {
                    context.Appointments.Add(new Appointment
                    {
                        PatientRecordId = 3,
                        DoctorId = 3,
                        Date = DateOnly.FromDateTime(DateTime.Now), // Ngày hiện tại
                        Time = TimeOnly.FromDateTime(DateTime.Now)  // Giờ hiện tại
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
