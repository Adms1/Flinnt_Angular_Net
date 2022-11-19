using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Repositories
{
    public class InstituteDivisionRepository : BaseRepository<InstituteDivision>, IInstituteDivisionRepository
    {
        public InstituteDivisionRepository(edplexdbContext context) : base(context)
        {
        }

        public Task<List<InstituteDivisionViewModel>> GetInstituteDivisionRecord(int instituteId)
        {
            return (from ig in Context.InstituteGroups
                    from d in Context.InstituteDivisions
                    from s in Context.Standards
                    where ig.InstituteGroupId == d.InstituteGroupId
                        && ig.StandardId == s.StandardId
                        && ig.InstituteId == instituteId
                    select new InstituteDivisionViewModel
                    {
                        InstituteGroupId = ig.InstituteGroupId,
                        DisplayOrder = d.DisplayOrder,
                        DivisionName = d.DivisionName,
                        InstituteDivisionId = d.InstituteDivisionId,
                        StandardViewModel = new StandardViewModel
                        {
                            StandardId = s.StandardId,
                            StandardName = s.StandardName,
                        }
                    }).ToListAsync();
        }
    }
}