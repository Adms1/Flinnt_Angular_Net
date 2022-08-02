using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using Flinnt.Repositories;
using System;

namespace Flinnt.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlinntContext Context;

        public UnitOfWork(FlinntContext context)
        {
            this.Context = context;
            AccountRepository = new AccountRepository(Context);
        }

        public IAccountRepository AccountRepository { get; }

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