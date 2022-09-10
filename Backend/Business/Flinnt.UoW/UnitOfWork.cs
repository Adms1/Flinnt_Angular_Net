using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using Flinnt.Repositories;
using System;

namespace Flinnt.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly edplexdbContext Context;

        public UnitOfWork(edplexdbContext context)
        {
            this.Context = context;
            InstituteRepository = new InstituteRepository(context);
            UserRepository = new UserRepository(context);
            UserProfileRepository = new UserProfileRepository(context);
            CityRepository = new CityRepository(context);
            StateRepository = new StateRepository(context);
            CountryRepository = new CountryRepository(context);
            UserRoleRepository = new UserRoleRepository(context);
            UserAccountHistoryRepository = new UserAccountHistoryRepository(context);
            UserAccountVerificationRepository = new UserAccountVerificationRepository(context);
            UserInstituteRepository = new UserInstituteRepository(context);
            UserSettingRepository = new UserSettingRepository(context);
        }

        public IInstituteRepository InstituteRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }
        public ICityRepository CityRepository { get; }
        public IStateRepository StateRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IUserAccountHistoryRepository UserAccountHistoryRepository { get; }
        public IUserAccountVerificationRepository UserAccountVerificationRepository { get; }
        public IUserInstituteRepository UserInstituteRepository { get; }
        public IUserSettingRepository UserSettingRepository { get; }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                Context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}