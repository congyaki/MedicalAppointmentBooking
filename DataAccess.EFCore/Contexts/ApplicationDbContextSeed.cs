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
                

                // Check for existing Users
                int existingUsersCount = context.Users.Count();

                // Add Users if less than 10 exist
                if (existingUsersCount < 20)
                {
                    for (int i = existingUsersCount + 1; i <= 10; i++)
                    {
                        var user = new User
                        {
                            UserName = $"user{i}",
                            Email = $"user{i}@example.com",
                            FirstName = $"John{i}",
                            LastName = $"Cena{i}",
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true
                        };

                        // Set the default password
                        var passwordHasher = new PasswordHasher<User>();
                        user.PasswordHash = passwordHasher.HashPassword(user, "Pa$$w0rd.");
                        context.Users.Add(user);
                    }
                    context.SaveChanges();

                }

                var maxCustomerId = context.Customers.Any();

                // Check for existing Customers
                if (!maxCustomerId)
                {
                    var existingUsers = context.Users.ToList(); // Get existing Users
                    for (int i = 1; i < 10; i++)
                    {
                        var randomUserId = existingUsers[new Random().Next(4, existingUsers.Count)].Id;
                        context.Customers.Add(new Customer
                        {
                            UserId = randomUserId
                        });
                    }
                    context.SaveChanges();
                }

                var maxCustomerIds = context.Customers.Max(c => c.Id);


                var maxPatientRecordId = context.PatientRecords.Any();
                // Check for existing PatientRecords
                if (!maxPatientRecordId)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        context.PatientRecords.Add(new PatientRecord
                        {
                            CustomerId = new Random().Next(1, maxCustomerIds), // Random customer ID

                            FirstName = $"Patient {i + 1} FirstName",
                            LastName = $"Patient {i + 1} LastName",
                            DateOfBirth = DateTime.Now.AddYears(-(20 + i)), // Random DoB within range
                            Gender = (i % 2 == 0) ? "Male" : "Female", // Alternate genders
                            Address = $"123 Main St {i}, Springfield",
                            PhoneNumber = $"123-456-{i}{i}{i}{i}",
                            Email = $"patient{i}@example.com"
                        });
                    }
                    context.SaveChanges();
                }


                var maxAppointmentId = context.Appointments.Any();

                if (!maxAppointmentId)
                {
                    for(int i = 1;i < 10; i++)
                    {
                        context.Appointments.Add(new Appointment
                        {
                            PatientRecordId = 3,
                            DoctorId = 3,
                            Date = DateOnly.FromDateTime(DateTime.Now), // Ngày hiện tại
                            Time = TimeOnly.FromDateTime(DateTime.Now)  // Giờ hiện tại
                        });
                    }

                    context.SaveChanges();
                }

                // Check for existing Doctors
                var maxDoctorId = context.Doctors.Any();

                // Add Doctors if less than 10 exist
                if (!maxDoctorId)
                {
                    var existingUsers = context.Users.ToList(); // Get existing Users

                    for (int i = 1; i <= 10; i++)
                    {
                        // Randomly select an existing User ID
                        var randomUserId = existingUsers[new Random().Next(0, existingUsers.Count)].Id;

                        context.Doctors.Add(new Doctor
                        {
                            UserId = randomUserId, // Link to existing User ID
                            Experience = 5,
                            Title = "Dr.",
                            Avatar = $"avatar{i}.png",
                            PhoneNumber = $"999-888-{i}{i}{i}{i}",
                            Address = $"1 Main St, City {i}",
                            DateOfBirth = DateTime.Now.AddYears(-(30 + i))
                        });
                    }
                    context.SaveChanges();
                }// Check for existing Doctors


                var maxSpecializationId = context.Specializations.Any();

                // Add Specialization if less than 10 exist
                if (!maxSpecializationId)
                {

                    for (int i = 1; i <= 10; i++)
                    {
                        context.Specializations.Add(new Specialization
                        {
                            Name = $"Specialization {i}",
                            Description = $"Description {i}",
                        });
                    }
                    context.SaveChanges();
                }

                var doctorsWithSpecializations = context.Doctors
                      .Include(d => d.DoctorSpecializations) // Eagerly load DoctorSpecializations
                      .Where(d => !d.DoctorSpecializations.Any()) // Filter doctors without specializations
                      .ToList();

                var specializations = context.Specializations.ToList(); // Get all specializations

                foreach (var doctor in doctorsWithSpecializations)
                {
                    // Assign 1-2 random specializations to each doctor
                    for (int i = 0; i < new Random().Next(1, 3); i++)
                    {
                        var randomSpecialization = specializations[new Random().Next(0, specializations.Count)];
                        doctor.DoctorSpecializations.Add(new DoctorSpecialization
                        {
                            DoctorId = doctor.Id,
                            SpecializationId = randomSpecialization.Id
                        });
                    }
                }

                context.SaveChanges();

                
            }
        }
    }
}
