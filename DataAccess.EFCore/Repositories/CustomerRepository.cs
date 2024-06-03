using Domain.Entities;
using Domain.Interfaces;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetByUserId(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
            var customerId = customer.Id;
            if (customer == null)
            {
                return 0;
            }
            return customerId;

        }
    }
}
