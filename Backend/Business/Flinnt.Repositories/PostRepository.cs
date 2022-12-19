using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Flinnt.Business.ViewModels;

namespace Flinnt.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(edplexdbContext context) : base(context)
        {
        }

        public Task<List<PostViewModel>> GetFeed(int instituteId)
        {
            return (from p in Context.Posts
                    where p.InstituteId == instituteId
                    select new PostViewModel
                    {
                        
                    }).ToListAsync();
        }
    }
}