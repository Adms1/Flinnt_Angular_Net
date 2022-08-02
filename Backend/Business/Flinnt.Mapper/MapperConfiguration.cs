using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;

namespace Flinnt.Business.Helpers
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<AccountModel, Account>();
            CreateMap<Account, AccountModel>();
        }
    }
}