using AutoMapper;
using Blessmate.Models;
using Blessmate.Records;

namespace Blessmate.Helpers
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
       {
            CreateMap<Register,Therpist>();
       }
    }
}