using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserSettingRepository : BaseRepository<UserSetting>, IUserSettingRepository
    {
        public UserSettingRepository(edplexdbContext context) : base(context)
        {
        }
    }
}