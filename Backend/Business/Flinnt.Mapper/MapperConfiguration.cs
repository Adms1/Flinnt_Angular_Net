using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;

namespace Flinnt.Business.Helpers
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<InstituteModel, Institute>();
            CreateMap<Institute, InstituteModel>();

            CreateMap<ApplicationUser, User>();
            CreateMap<User, ApplicationUser>();

            CreateMap<UserProfileModel, UserProfile>();
            CreateMap<UserProfile, UserProfileModel>();

            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>();

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>();

            CreateMap<StateViewModel, State>();
            CreateMap<State, StateViewModel>();

            CreateMap<InstituteTypeViewModel, InstituteType>();
            CreateMap<InstituteType, InstituteTypeViewModel>();

            CreateMap<GroupStructureViewModel, GroupStructure>();
            CreateMap<GroupStructure, GroupStructureViewModel>();

            CreateMap<BoardViewModel, Board>();
            CreateMap<Board, BoardViewModel>();

            CreateMap<MediumViewModel, Medium>();
            CreateMap<Medium, MediumViewModel>();

            CreateMap<StandardViewModel, Standard>();
            CreateMap<Standard, StandardViewModel>();

            CreateMap<InstituteGroupViewModel, InstituteGroup>();
            CreateMap<InstituteGroup, InstituteGroupViewModel>();

            CreateMap<InstituteDivisionViewModel, InstituteDivision>();
            CreateMap<InstituteDivision, InstituteDivisionViewModel>();

            CreateMap<InstituteConfigureSessionViewModel, InstituteConfigureSession>();
            CreateMap<InstituteConfigureSession, InstituteConfigureSessionViewModel>();
        }
    }
}