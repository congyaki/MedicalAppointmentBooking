using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using MedicalAppointmentBooking.WebAPI.Interfaces;
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
    public class PatientRecordRepository : GenericRepository<PatientRecord>, IPatientRecordRepository
    {

        private readonly MedicalAppointmentBookingDbContext _context;
        public PatientRecordRepository(MedicalAppointmentBookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PatientRecord>> GetPatientRecordOfCustomer(int customerId)
        {
            return await _context.PatientRecords
            .Where(pr => pr.CustomerId == customerId)
            .ToListAsync();
        }
    }
}
