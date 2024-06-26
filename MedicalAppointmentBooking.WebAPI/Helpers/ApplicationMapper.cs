﻿using AutoMapper;
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
            CreateMap<Doctor, DoctorBasicVM>();
            /*.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Thêm ánh xạ cho Id
            .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience)) // Thêm ánh xạ cho Experience
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));*/ // Thêm ánh xạ cho Avatar
            CreateMap<DoctorSpecialization, Doctor>();
        }
    }
}
