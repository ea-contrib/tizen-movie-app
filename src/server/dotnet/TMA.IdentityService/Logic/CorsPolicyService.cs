using System.Threading.Tasks;
using IdentityServer4.Services;

namespace TMA.IdentityService.Logic
{
    public class CorsPolicyService: ICorsPolicyService
    {
        private readonly ICorsClientStore _clientStore;

        public CorsPolicyService(ICorsClientStore clientStore)
        {
            _clientStore = clientStore;
        }
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return _clientStore.IsOriginAllowedAsync(origin);
        }
    }
}