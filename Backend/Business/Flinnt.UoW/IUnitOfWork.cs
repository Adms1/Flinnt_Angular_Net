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
    }
}