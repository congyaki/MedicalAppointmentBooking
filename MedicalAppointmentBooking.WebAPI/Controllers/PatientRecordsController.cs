using AutoMapper;
using Domain.Interfaces;
using Domain.ViewModels;
using MedicalAppointmentBooking.WebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRecordsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //Nen bo
        private readonly IUserService _userService;

        public PatientRecordsController(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //Nen bo
            _userService = userService;
        }

        [HttpGet, Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<List<PatientRecordVM>>> GetPatientRecordOfCustomer()
        {
            var userId = User.FindFirstValue("uid");
            var customerId = await _unitOfWork.Customers.GetByUserId(userId);
            var patientRecords = await _unitOfWork.PatientRecords.GetPatientRecordOfCustomer(customerId);
            var respond = _mapper.Map<List<PatientRecordVM>>(patientRecords);

            return Ok(respond);
        }
    }
}
