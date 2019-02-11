using System;

namespace GeoStat.BussinessLogic.Access
{
    public interface ITokenManager
    {
        string GenerateToken(
            string userName,
            string userId,
            DateTime? notBefore = null,
            DateTime? expires = null);

        bool Validate(string token);
    }
}
