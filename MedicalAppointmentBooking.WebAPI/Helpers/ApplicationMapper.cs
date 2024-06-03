using AutoMapper;
using Domain.Entities;
using Domain.ViewModels;
using MedicalAppointmentBooking.WebAPI.Models.Entities;
using MedicalAppointmentBooking.WebAPI.ViewModels;

namespace MedicalAppointmentBooking.WebAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Specialization, SpecializationVM>().ReverseMap();
            CreateMap<Doctor, DoctorVM>().ForMember(dest => dest.Specializations, opt => opt.MapFrom(src => src.DoctorSpecializations.Select(ds => ds.Specialization))).ReverseMap();
            CreateMap<AddDoctorVM, Doctor>().ReverseMap();
            CreateMap<PatientRecord, PatientRecordVM>().ReverseMap();
            CreateMap<Appointment, AppointmentVM>().ReverseMap();
        }
    }
}
