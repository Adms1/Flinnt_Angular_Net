using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Repositories
{
    public interface IParentRepository : IBaseRepository<Parent>
    {
        Parent CreateParentRecord(Parent parent);
        List<ParentViewModel> GetAll();
    }
}