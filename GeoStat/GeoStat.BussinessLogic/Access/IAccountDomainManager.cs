using GeoStat.DTO;
using System.Threading.Tasks;

namespace GeoStat.BussinessLogic.Access
{
    public interface IAccountDomainManager
    { 
        Task<AuthResult> Register(AuthorisationUserDTO userDTO);

        Task<AuthResult> Authorise(AuthorisationUserDTO userDTO);
    }
}
