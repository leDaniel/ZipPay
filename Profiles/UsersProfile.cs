using AutoMapper;
using ZipPay.Dtos;
using ZipPay.Models;

namespace ZipPay.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }

    }
}