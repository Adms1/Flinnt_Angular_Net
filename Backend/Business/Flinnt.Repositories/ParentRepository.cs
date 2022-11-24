using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Repositories
{
    public class ParentRepository : BaseRepository<Parent>, IParentRepository
    {
        public ParentRepository(edplexdbContext context) : base(context)
        {

        }

        public Parent CreateParentRecord(Parent record)
        {
            Context.Parents.Add(record);
            Context.SaveChanges();
            return record;
        }

        public List<ParentViewModel> GetAll()
        {
            var cities = Context.Cities.ToList();
            var states = Context.States.ToList();
            var counties = Context.Countries.ToList();
            var parent = Context.Parents.AsEnumerable().Select(x=> 
                new ParentViewModel
                {
                    Parent1FirstName = x.Parent1FirstName,
                    Parent1LastName = x.Parent1LastName,
                    Parent2FirstName = x.Parent2FirstName,
                    Parent2LastName = x.Parent2LastName,
                    UserId = x.UserId,
                    SingleParent = x.SingleParent,
                    ParentId = x.ParentId,
                    Parent1Relationship = x.Parent1Relationship,
                    Parent2Relationship = x.Parent2Relationship,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    Parent1EmailId = x.Parent1EmailId,
                    PrimaryMobileNo = x.PrimaryMobileNo,
                    PrimaryEmailId = x.PrimaryEmailId,
                    Parent1MobileNo = x.Parent1MobileNo,
                    Parent2MobileNo = x.Parent2MobileNo,
                    Parent2EmailId = x.Parent2EmailId,
                    Pincode=x.Pincode, 
                    CityName = x.CityId != null ? cities.Where(y => y.CityId == x.CityId.Value).FirstOrDefault().CityName: "",
                    StateName = x.StateId != null ? states.Where(y => y.StateId == x.StateId.Value).FirstOrDefault().StateName : "",
                    CountryName = x.CountryId != null ? counties.Where(y => y.CountryId == x.CountryId.Value).FirstOrDefault().CountryName : "",
                }
            ).ToList();
            return parent;
        }
    }
}