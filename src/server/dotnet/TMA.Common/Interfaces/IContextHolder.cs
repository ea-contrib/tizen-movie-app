using System.Security.Claims;

namespace TMA.Common.Interfaces
{
    public interface IContextHolder
    {
        ClaimsPrincipal User { get; set; }
    }
}
