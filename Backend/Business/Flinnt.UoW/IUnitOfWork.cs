using Flinnt.Interfaces.Repositories;
using System;

namespace Flinnt.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
    }
}