using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApi.Middleware
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        Task<bool> IsActiveAsync(string token);
    }
}
