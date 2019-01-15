using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class BaseController<TData> : TableController<TData> where TData : class, ITableData
    {
        public BaseController(IDomainManager<TData> manager)
        {
            this.DomainManager = manager;
        }
    }
}
