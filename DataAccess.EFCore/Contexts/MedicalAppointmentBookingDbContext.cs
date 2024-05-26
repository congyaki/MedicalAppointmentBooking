using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entities;
using DataAccess.EFCore.Converter;

namespace MedicalAppointmentBooking.WebAPI.Models.EF
{
    public class MedicalAppointmentBookingDbContext : IdentityDbContext<User>
    {
        public MedicalAppointmentBookingDbContext(DbContextOptions<MedicalAppointmentBookingDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PatientRecord> PatientRecords { get; set; }
        public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(e => e.Id);

            modelBuilder.Entity<Specialization>().HasKey(e => e.Id);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User).WithOne(e => e.Doctor).HasForeignKey<Doctor>(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User).WithOne(e => e.Customer).HasForeignKey<Customer>(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DoctorSpecialization>(entity =>
            {
                entity.HasKey(e => new { e.DoctorId, e.SpecializationId });

                entity.HasOne(e => e.Doctor).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.DoctorId);
                entity.HasOne(e => e.Specialization).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.SpecializationId);
            });

            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Customer).WithMany(e => e.PatientRecords).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Doctor).WithMany(e => e.Appointments).HasForeignKey(e => e.DoctorId).OnDelete(DeleteBehavior.Restrict); ;
                entity.HasOne(e => e.PatientRecord).WithMany(e => e.Appointments).HasForeignKey(e => e.PatientRecordId).OnDelete(DeleteBehavior.Restrict); ;
                entity.Property(e => e.Date)
                .HasConversion<DateOnlyConverter>()
                .HasColumnType("date");

                entity.Property(e => e.Time)
                    .HasConversion<TimeOnlyConverter>()
                    .HasColumnType("time");
            });

        }
    }
}
