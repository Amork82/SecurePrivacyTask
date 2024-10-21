using AutoMapper;
using SecurePrivacyTask.Server.Dto.Input;
using SecurePrivacyTask.Server.Dto.Output;
using SecurePrivacyTask.Server.Models;

namespace SecurePrivacyTask.Server.Commons
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDtoOutput>();
        }
    }
}
