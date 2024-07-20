using AutoMapper;
using ChatApp.Data;
using ChatApp.Services.Models.User;

namespace ChatApp.Services.Mapping
{
    public class MyProfiles : Profile
    {
        public MyProfiles()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserCreateModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();
            // CreateMap<Group, GroupDto>().ReverseMap();
            // CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}