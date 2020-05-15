using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TMA.IdentityService.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string ConnectPathPrefix = "/connect";
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers.TryGetValue("X-Requested-With", out StringValues headerValue))
            {
                return headerValue.Any(x =>
                    x.Equals("XMLHttpRequest", StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        public static bool IsIdpEndpoint(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var connectPath = request.PathBase.Add(new PathString(ConnectPathPrefix));
            
            return request.Path.StartsWithSegments(connectPath);
        }
    }
}
