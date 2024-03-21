using MedicalAppointmentBooking.WebAPI.Entities;
using MedicalAppointmentBooking.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBooking.WebAPI.EF
{
    public class MedicalAppointmentBookingDbContext : DbContext
    {
        public MedicalAppointmentBookingDbContext(DbContextOptions<MedicalAppointmentBookingDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);

            modelBuilder.Entity<Specialization>().HasKey(e => e.Id);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User).WithMany(e => e.Doctors).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DoctorSpecialization>(entity =>
            {
                entity.HasKey(e => new { e.DoctorID, e.SpecializationID });

                entity.HasOne(e => e.Doctor).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Specialization).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
