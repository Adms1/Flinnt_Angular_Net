using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;

namespace Flinnt.Background
{
    public class BackgroundStudentJobs : IBackgroundStudentJobs
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly ICityService _cityService;
        private readonly IUserAccountHistoryService _userAccountHistoryService;

        #region Constructor

        public BackgroundStudentJobs(IStudentService studentService,
            IUserService userService,
            IUserInstituteService userInstituteService,
            ICityService cityService,
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService)
        {
            _studentService = studentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userProfileService = userProfileService;
            _userAccountHistoryService = userAccountHistoryService;
        }
        #endregion Constructor
        
        public void ImportStudents(List<StudentViewModel> parentViewModels)
        {
            
        }
    }
}