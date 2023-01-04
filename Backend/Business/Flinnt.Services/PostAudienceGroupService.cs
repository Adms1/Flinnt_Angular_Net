using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class PostAudienceGroupService : ServiceBase, IPostAudienceGroupService
    {
        public PostAudienceGroupService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostAudienceGroupViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<PostAudienceGroupViewModel>>(await unitOfWork.PostAudienceGroupRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<PostAudienceGroupViewModel> GetAsync(int id)
        {
            return mapper.Map<PostAudienceGroupViewModel>(await unitOfWork.PostAudienceGroupRepository.GetAsync(id));
        }

        public async Task<List<PostAudienceGroupViewModel>> GetPostAudienceGroupByInstituteIdAndUserId(int instituteId, int userId)
        {
            return await Task.FromResult(await unitOfWork.PostAudienceGroupRepository.GetPostAudienceGroupByInstituteIdAndUserId(instituteId, userId));
        }

        public async Task<bool> AddAsync(PostAudienceGroupViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostAudienceGroupRepository.AddAsync(mapper.Map<PostAudienceGroupViewModel, PostAudienceGroup>(model)));
            if (data.AudienceGroupId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostAudienceGroupViewModel model)
        {
            var postAudienceGroup = await unitOfWork.PostAudienceGroupRepository.GetAsync(model.AudienceGroupId);
            if (postAudienceGroup != null)
            {
                postAudienceGroup.FilterData = model.FilterData;
                postAudienceGroup.GroupName = model.GroupName;
                postAudienceGroup.GroupLogo = model.GroupLogo;

                await unitOfWork.PostAudienceGroupRepository.UpdateAsync(postAudienceGroup);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var postAudienceGroup = unitOfWork.PostAudienceGroupRepository.GetAsync(id).Result;
            if (postAudienceGroup != null)
            {
                await unitOfWork.PostAudienceGroupRepository.DeleteAsync(postAudienceGroup);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}