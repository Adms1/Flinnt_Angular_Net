using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using Flinnt.Repositories;
using System;
using System.Linq.Expressions;

namespace Flinnt.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly edplexdbContext Context;

        public UnitOfWork(edplexdbContext context)
        {
            this.Context = context;
            InstituteRepository = new InstituteRepository(context);
            UserRepository = new UserRepository(context);
            UserProfileRepository = new UserProfileRepository(context);
            CityRepository = new CityRepository(context);
            StateRepository = new StateRepository(context);
            CountryRepository = new CountryRepository(context);
            UserRoleRepository = new UserRoleRepository(context);
            UserAccountHistoryRepository = new UserAccountHistoryRepository(context);
            UserAccountVerificationRepository = new UserAccountVerificationRepository(context);
            UserInstituteRepository = new UserInstituteRepository(context);
            UserSettingRepository = new UserSettingRepository(context);
            LoginHistoryRepository = new LoginHistoryRepository(context);
            GroupStructureRepository = new GroupStructureRepository(context);
            BoardRepository = new BoardRepository(context);
            MediumRepository = new MediumRepository(context);
            StandardRepository = new StandardRepository(context);
            InstituteGroupRepository = new InstituteGroupRepository(context);
            InstituteDivisionRepository = new InstituteDivisionRepository(context);
            InstituteTypeRepository = new InstituteTypeRepository(context);
            InstituteConfigureSessionRepository = new InstituteConfigureSessionRepository(context);
            ParentRepository = new ParentRepository(context);
            StudentRepository = new StudentRepository(context);
            UserInstituteGroupRepository = new UserInstituteGroupRepository(context);
            UserParentChildRelationshipRepository = new UserParentChildRelationshipRepository(context);
            PostRepository = new PostRepository(context);
            PostCommentRepository = new PostCommentRepository(context);
            PostAudienceGroupRepository = new PostAudienceGroupRepository(context);
            PostLogRepository = new PostLogRepository(context);
            PostMediaRepository = new PostMediaRepository(context);
            PostPollOptionRepository = new PostPollOptionRepository(context);
            PostPollRepository = new PostPollRepository(context);
            PostPollVoteRepository = new PostPollVoteRepository(context);
            PostPollVoteSummaryRepository = new PostPollVoteSummaryRepository(context);
            PostTemplateRepository = new PostTemplateRepository(context);
            PostTemplateCategoryRepository = new PostTemplateCategoryRepository(context);
            PostTypeRepository = new PostTypeRepository(context);
            PostUserRepository = new PostUserRepository(context);
            MediaEmbedRepository = new MediaEmbedRepository(context);
            MediaTypeRepository = new MediaTypeRepository(context);
        }

        public IInstituteRepository InstituteRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }
        public ICityRepository CityRepository { get; }
        public IStateRepository StateRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IUserAccountHistoryRepository UserAccountHistoryRepository { get; }
        public IUserAccountVerificationRepository UserAccountVerificationRepository { get; }
        public IUserInstituteRepository UserInstituteRepository { get; }
        public IUserSettingRepository UserSettingRepository { get; }
        public ILoginHistoryRepository LoginHistoryRepository { get; }
        public IInstituteTypeRepository InstituteTypeRepository { get; }
        public IGroupStructureRepository GroupStructureRepository { get; }
        public IBoardRepository BoardRepository { get; }
        public IMediumRepository MediumRepository { get; }
        public IStandardRepository StandardRepository { get; }
        public IInstituteGroupRepository InstituteGroupRepository { get; }
        public IInstituteDivisionRepository InstituteDivisionRepository { get; }
        public IInstituteConfigureSessionRepository InstituteConfigureSessionRepository { get; }
        public IParentRepository ParentRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IUserInstituteGroupRepository UserInstituteGroupRepository { get; }
        public IUserParentChildRelationshipRepository UserParentChildRelationshipRepository { get; }
        public IPostRepository PostRepository { get; }
        public IPostCommentRepository PostCommentRepository { get; }
        public IPostAudienceGroupRepository PostAudienceGroupRepository { get; }
        public IPostLogRepository PostLogRepository { get; }
        public IPostMediaRepository PostMediaRepository { get; }
        public IPostPollOptionRepository PostPollOptionRepository { get; }
        public IPostPollRepository PostPollRepository { get; }
        public IPostPollVoteRepository PostPollVoteRepository { get; }
        public IPostPollVoteSummaryRepository PostPollVoteSummaryRepository { get; }
        public IPostTemplateRepository PostTemplateRepository { get; }
        public IPostTemplateCategoryRepository PostTemplateCategoryRepository { get; }
        public IPostTypeRepository PostTypeRepository { get; }
        public IPostUserRepository PostUserRepository { get; }
        public IMediaEmbedServiceRepository MediaEmbedRepository { get; }
        public IMediaTypeRepository MediaTypeRepository { get; }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                Context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}