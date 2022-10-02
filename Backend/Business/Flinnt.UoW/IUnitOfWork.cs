using Flinnt.Interfaces.Repositories;
using System;

namespace Flinnt.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IInstituteRepository InstituteRepository { get; }
        IUserRepository UserRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        ICityRepository CityRepository { get; }
        IStateRepository StateRepository { get; }
        ICountryRepository CountryRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IUserAccountHistoryRepository UserAccountHistoryRepository { get; }
        IUserAccountVerificationRepository UserAccountVerificationRepository { get; }
        IUserInstituteRepository UserInstituteRepository { get; }
        IUserSettingRepository UserSettingRepository { get; }
        ILoginHistoryRepository LoginHistoryRepository { get; }
        IGroupStructureRepository GroupStructureRepository { get; }
        IBoardRepository BoardRepository { get; }
        IMediumRepository MediumRepository { get; }
        IStandardRepository StandardRepository { get; }
        IInstituteGroupRepository InstituteGroupRepository { get; }
        IInstituteDivisionRepository InstituteDivisionRepository { get; }
    }
}