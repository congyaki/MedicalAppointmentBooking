﻿using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalAppointmentBooking.WebAPI.Models.EF
{
    public class MedicalAppointmentBookingDbContext : IdentityDbContext<User>
    {
        public MedicalAppointmentBookingDbContext(DbContextOptions<MedicalAppointmentBookingDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(e => e.Id);

            modelBuilder.Entity<Specialization>().HasKey(e => e.Id);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id);

                /*entity.HasOne(e => e.User).WithOne(e => e.Doctor).OnDelete(DeleteBehavior.Cascade);*/
            });

            modelBuilder.Entity<DoctorSpecialization>(entity =>
            {
                entity.HasKey(e => new { e.DoctorId, e.SpecializationId });

                entity.HasOne(e => e.Doctor).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.DoctorId);
                entity.HasOne(e => e.Specialization).WithMany(e => e.DoctorSpecializations).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.SpecializationId);
            });

        }
    }
}