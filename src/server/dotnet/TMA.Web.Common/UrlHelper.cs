using System;
using System.Collections.Generic;

namespace TMA.Web.Common
{
    public static class UrlHelper
    {
        public static List<string> GenerateDomainUrls(string relativeAppUrl, List<string> applicationDomains)
        {
            var urls = new List<string>();

            foreach (var applicationDomain in applicationDomains)
            {
                var domain = applicationDomain;
                if (!domain.Contains("http", StringComparison.InvariantCultureIgnoreCase))
                {
                    domain = $"http://{domain}";
                }

                var domainUri = new Uri(domain);
                if (domainUri.IsDefaultPort)
                {
                    UriBuilder builder = new UriBuilder(domain)
                    {
                        Port = -1,
                        Scheme = Uri.UriSchemeHttp
                    };
                    var httpUrl = builder.ToString();
                    builder.Scheme = Uri.UriSchemeHttps;
                    var httpsUrl = builder.ToString();

                    urls.Add(new Uri(new Uri(httpUrl),
                            new Uri(relativeAppUrl,
                                UriKind.Relative))
                        .ToString());

                    urls.Add(new Uri(new Uri(httpsUrl),
                            new Uri(relativeAppUrl,
                                UriKind.Relative))
                        .ToString());
                }
                else
                {
                    urls.Add(new Uri(domainUri, new Uri(relativeAppUrl, UriKind.Relative))
                        .ToString());
                }
            }

            return urls;
        }
    }
}
