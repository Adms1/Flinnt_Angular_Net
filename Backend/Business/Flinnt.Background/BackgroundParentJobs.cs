using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;

namespace Flinnt.Background
{
    public class BackgroundParentJobs : IBackgroundParentJobs
    {
        private readonly IParentService _parentService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly ICityService _cityService;
        private readonly IUserAccountHistoryService _userAccountHistoryService;

        #region Constructor

        public BackgroundParentJobs(IParentService parentService,
            IUserService userService,
            IUserInstituteService userInstituteService,
            ICityService cityService,
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService)
        {
            _parentService = parentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userProfileService = userProfileService;
            _userAccountHistoryService = userAccountHistoryService;
        }
        #endregion Constructor
        
        public void ImportParents(List<ParentViewModel> parentViewModels)
        {
            
        }
    }
}