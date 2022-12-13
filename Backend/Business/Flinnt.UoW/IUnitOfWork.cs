using Flinnt.Interfaces.Repositories;
using System;

namespace Flinnt.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IInstituteRepository InstituteRepository { get; }
        IUserRepository UserRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        ICityRepository CityRepository { get; }
        IStateRepository StateRepository { get; }
        ICountryRepository CountryRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IUserAccountHistoryRepository UserAccountHistoryRepository { get; }
        IUserAccountVerificationRepository UserAccountVerificationRepository { get; }
        IUserInstituteRepository UserInstituteRepository { get; }
        IUserSettingRepository UserSettingRepository { get; }
        ILoginHistoryRepository LoginHistoryRepository { get; }
        IInstituteTypeRepository InstituteTypeRepository { get; }
        IGroupStructureRepository GroupStructureRepository { get; }
        IBoardRepository BoardRepository { get; }
        IMediumRepository MediumRepository { get; }
        IStandardRepository StandardRepository { get; }
        IInstituteGroupRepository InstituteGroupRepository { get; }
        IInstituteDivisionRepository InstituteDivisionRepository { get; }
        IInstituteConfigureSessionRepository InstituteConfigureSessionRepository { get; }
        IParentRepository ParentRepository { get; }
        IStudentRepository StudentRepository { get; }
        IUserInstituteGroupRepository UserInstituteGroupRepository { get; }
        IUserParentChildRelationshipRepository UserParentChildRelationshipRepository { get; }
        IPostRepository PostRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostAudienceGroupRepository PostAudienceGroupRepository { get; }
        IPostLogRepository PostLogRepository { get; }
        IPostMediaRepository PostMediaRepository { get; }
        IPostPollOptionRepository PostPollOptionRepository { get; }
        IPostPollRepository PostPollRepository { get; }
        IPostPollVoteRepository PostPollVoteRepository { get; }
        IPostPollVoteSummaryRepository PostPollVoteSummaryRepository { get; }
        IPostTemplateRepository PostTemplateRepository { get; }
        IPostTemplateCategoryRepository PostTemplateCategoryRepository { get; }
        IPostTypeRepository PostTypeRepository { get; }
        IPostUserRepository PostUserRepository { get; }
        IMediaEmbedServiceRepository MediaEmbedRepository { get; }
        IMediaTypeRepository MediaTypeRepository { get; }
    }
}