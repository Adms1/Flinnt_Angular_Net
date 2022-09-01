using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;

namespace Flinnt.Business.Helpers
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<InstituteModel, Institute>();
            CreateMap<Institute, InstituteModel>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<UserProfileModel, UserProfile>();
            CreateMap<UserProfile, UserProfileModel>();

            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>();

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>();

            CreateMap<StateViewModel, State>();
            CreateMap<State, StateViewModel>();
        }
    }
}