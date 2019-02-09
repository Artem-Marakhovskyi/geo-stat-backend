using GeoStat.Entities;
using Microsoft.AspNet.Identity;

namespace GeoStat.BussinessLogic
{
    public class UserIdentityDomainManager : UserManager<User>
    {
        public UserIdentityDomainManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}