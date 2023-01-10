using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using System.Transactions;

namespace Flinnt.Background
{
    // TODO: Refactor Code to re-used everyware
    public class BackgroundPostJobs : IBackgroundPostJobs
    {
        public static IPostService _postService;
        public static IPostLogService _postLogService { get; set; }
        public static IUserService _userService { get; set; }
        public static IBackgroundMailerJobs _backgroundMailerJobs;

        #region Constructor

        public BackgroundPostJobs(
           IPostService postService,
           IPostLogService postLogService,
           IUserService userService,
           IBackgroundMailerJobs backgroundMailerJobs)
        {
            _postService = postService;
            _postLogService = postLogService;
            _userService = userService;
            _backgroundMailerJobs = backgroundMailerJobs;
        }
        #endregion Constructor

        public async Task ScheduledPostAsync(int postId)
        {
            try
            {
                string userEmailId = "";
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var post = await _postService.GetAsync(postId);
                        if (post == null)
                        {
                        }
                        else
                        {
                            post.PublishDateTime = DateTime.Now;
                            await _postService.UpdateAsync(post);

                            await _postLogService.AddAsync(new PostLogViewModel
                            {
                                PostId = postId,
                                UserId = post.UserId,
                                ActionType = "PostPublished",
                                ActionDateTime = DateTime.Now,
                                ExtraInformation = "NA",
                                ClientIp = "127.0.0.1",
                                ClientDevice = "BackgroundJob"
                            });

                            //get user
                            var user = await _userService.GetAsync(Convert.ToInt64(post.UserId));

                            if(user != null)
                            {
                                userEmailId = user.LoginId;
                            }
                        }

                        
                        scope.Complete();
                        scope.Dispose();
                    }
                    catch (TransactionException tex)
                    {
                        scope.Dispose();
                        throw;
                    }
                }

                //send summary email
                _backgroundMailerJobs.SendScheduledJobEmail("Post published", userEmailId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
