using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using GeoStat.DataAccess;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.BussinessLogic
{
    public class BaseDomainManager<TData, TModel> : MappedEntityDomainManager<TData, TModel>
        where TData : class, Microsoft.Azure.Mobile.Server.Tables.ITableData
        where TModel : class, Microsoft.Azure.Mobile.Server.Tables.ITableData
    {
        public BaseDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
            Context = geoStatContext;
        }

        public override Task<bool> DeleteAsync(string id)
        {
            return DeleteItemAsync(id);
        }

        public override SingleResult<TData> Lookup(string id)
        {
            return LookupEntity(model => model.Id == id);
        }

        public override Task<TData> UpdateAsync(string id, Delta<TData> patch)
        {
            return this.UpdateEntityAsync(patch, id);
        }
    }
}
