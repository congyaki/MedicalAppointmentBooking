using Domain.Entities;
using Domain.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly MedicalAppointmentBookingDbContext _context;
        public CustomerRepository(MedicalAppointmentBookingDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
