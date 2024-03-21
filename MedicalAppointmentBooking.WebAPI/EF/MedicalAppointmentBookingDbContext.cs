using MedicalAppointmentBooking.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBooking.WebAPI.EF
{
    public class MedicalAppointmentBookingDbContext : DbContext
    {
        public MedicalAppointmentBookingDbContext(DbContextOptions<MedicalAppointmentBookingDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
        }
    }
}
