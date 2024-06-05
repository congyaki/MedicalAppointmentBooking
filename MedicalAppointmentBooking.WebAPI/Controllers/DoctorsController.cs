using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.Models.EF;
using MedicalAppointmentBooking.WebAPI.ViewModels;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using AutoMapper;
using Domain.ViewModels;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _UnitOfWork = unitOfWork;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorVM>>> GetDoctors()
        {
            var respond = _UnitOfWork.Doctors.GetAll();
            return Ok (respond);
        }
        [HttpGet("GetDoctorsBySpecizalizationId/{specializationId}")]
        public async Task<ActionResult<List<DoctorBasicVM>>> GetDoctorsBySpecizalizationId(int specializationId)
        {
            var doctorsBasicVM = await _UnitOfWork.Specializations.GetDoctorInSpecialization(specializationId);
            return Ok(doctorsBasicVM);
        }
        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var respond = _UnitOfWork.Doctors.GetById(id);
            return respond;
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(AddDoctorVM addDoctorVM)
        {
            var respond = _UnitOfWork.Doctors.AddDoctorWithSpecialization(addDoctorVM);
            return Ok(respond);
        }

    }
}
