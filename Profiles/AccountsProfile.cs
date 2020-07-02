using AutoMapper;
using ZipPay.Dtos;
using ZipPay.Models;

namespace ZipPay.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountCreateDto, Account>();
        }

    }
}