using AutoMapper;
using Blessmate.DTOs;
using Blessmate.Models;
using Blessmate.Records;

namespace Blessmate.Helpers
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
       {
            CreateMap<Register,Patient>();
            CreateMap<TherapistRegister,Therapist>();
            CreateMap<Therapist,TherapistData>();
            CreateMap<Appointment,AppointmentDto>();
            CreateMap<Appointment,TherapistData>()
                .IncludeMembers(src => src.Therapist)
               .ForMember(dest => dest.Id , opt => opt.MapFrom(src => src.Therapist.Id));
    
       }
    }
}