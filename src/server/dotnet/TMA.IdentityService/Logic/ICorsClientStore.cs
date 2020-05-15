using System.Threading.Tasks;

namespace TMA.IdentityService.Logic
{
    public interface ICorsClientStore
    {
        Task<bool> IsOriginAllowedAsync(string origin);
    }
}