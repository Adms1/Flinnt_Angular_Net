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
        }

        public IInstituteRepository InstituteRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }

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