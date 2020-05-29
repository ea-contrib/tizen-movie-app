using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TMA.Common.Interfaces;

namespace TMA.Web.Common
{
    public class HttpContextHolder : IContextHolder
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public HttpContextHolder(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User
        {
            get => HttpContextAccessor?.HttpContext?.User;
            set => HttpContextAccessor.HttpContext.User = value;
        }
    }
}
