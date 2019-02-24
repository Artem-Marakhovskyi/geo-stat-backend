using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeoStat.WebAPI.Controllers
{
    public class GroupController : BaseController<GroupDto>
    {
        private readonly IGroupDomainManager _groupDomainManager;

        public GroupController(
            IGroupDomainManager manager)
            : base(manager)
        {
            _groupDomainManager = manager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GroupDto item)
        {
            var groupDto = await DomainManager.InsertAsync(item);
            return this.Ok(groupDto);
        }

        [HttpGet]
        public IQueryable<GroupDto> Get()
        {
            return this.Query();
        }

        [HttpGet]
        [Route("User/{groupId}")] //change route
        public IEnumerable<GeoStatUserDto> GetUserOfGroup(string groupId)
        {
            return _groupDomainManager.GetUsersOfGroup(groupId);
        }

        [HttpGet]
        [Route("Group/{userId}")]
        public IEnumerable<GroupModel> GetGroupsOfUser(string userId)
        {
            return _groupDomainManager.GetGroupsOfUser(userId);
        }
    }
}
