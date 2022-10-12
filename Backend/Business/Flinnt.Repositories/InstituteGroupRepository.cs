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
    public class InstituteGroupRepository : BaseRepository<InstituteGroup>, IInstituteGroupRepository
    {
        public InstituteGroupRepository(edplexdbContext context) : base(context)
        {

        }

        public Task<List<InstituteGroupViewModel>> GetInstituteGroupRecord(int instituteId)
        {
            return (from ig in Context.InstituteGroups where ig.InstituteId == instituteId
                   join s in Context.Standards on ig.StandardId equals s.StandardId into ss
                   from s in ss.DefaultIfEmpty()
                   select new InstituteGroupViewModel
                   {
                       StandardViewModel = new StandardViewModel
                       {
                           StandardId = s.StandardId,
                           StandardName = s.StandardName,
                       },
                       BoardId = ig.BoardId,
                       InstituteId = ig.InstituteId,
                       InstituteGroupId = ig.InstituteGroupId,
                       MediumId = ig.MediumId,
                       StandardId = s.StandardId,
                   }).ToListAsync();
        }
    }
}