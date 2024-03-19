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
       }
    }
}