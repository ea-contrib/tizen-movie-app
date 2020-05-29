using System;
using Microsoft.AspNetCore.Http;
using TMA.Common.Interfaces;

namespace TMA.Web.Common
{
    public class HttpRequestAware : IRequestAware
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestAware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public object this[string key]
        {
            get => _httpContextAccessor.HttpContext.Items.TryGetValue(key, out var value) ? value : null;
            set => _httpContextAccessor.HttpContext.Items[key] = value;
        }
    }
}
