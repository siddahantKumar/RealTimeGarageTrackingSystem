using AutoMapper;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;

namespace IndusShowroomApi.Profiles
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
           


            CreateMap<User_Type, User_TypeDto>();
            CreateMap<Charts_Of_Accounts, Charts_Of_AccountsDto>();
        }
    }
}
