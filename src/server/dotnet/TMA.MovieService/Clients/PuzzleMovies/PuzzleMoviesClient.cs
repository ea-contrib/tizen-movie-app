using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TMA.Common;

namespace TMA.MovieService.Clients.PuzzleMovies
{
    public class PuzzleMoviesClient
    {
        private readonly PuzzleMoviesClientConfigurationFactory _configuration;
        private readonly ILogger<PuzzleMoviesClient> _logger;
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });
        private string BaseUrl = "https://puzzle-movies.com/";
        private string TokenRequestRelativeUrl = "/api/auth.php";
        private string MovieListRelativeUrl = "/api2/movies/getList";


        public PuzzleMoviesClient(PuzzleMoviesClientConfigurationFactory configuration, ILogger<PuzzleMoviesClient> logger)
        {
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<ApiResponse<PuzzleMoviesMoviesListModel>> MoviesList(CancellationToken cancellationToken = default)
        {
            return GetJsonAsync<PuzzleMoviesMoviesListModel>(c =>
                {
                    c.RequestUri = GetFullUrl(MovieListRelativeUrl);

                    return EnrichRequestWithAccessTokenAsync(c, cancellationToken);
                }, cancellationToken);
        }

        #region Helpers

        private Uri GetFullUrl(string relativePart)
        {
            return new Uri($"{BaseUrl.RemoveTrailingSlash()}/{relativePart.TrimStart('/')}");
        }

        private async Task<ApiResponse<PuzzleMoviesTokenResponseModel>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            var config = await _configuration.CreateAsync();

            return await PostFormAsync<PuzzleMoviesTokenResponseModel>(c =>
            {
                c.RequestUri = GetFullUrl(TokenRequestRelativeUrl);
                return Task.FromResult(new ApiResponse());
            }, new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", config.Login),
                new KeyValuePair<string, string>("password", config.Password),
            }, cancellationToken);
        }

        private async Task<ApiResponse<T>> PostFormAsync<T>(Func<HttpRequestMessage, Task<ApiResponse>> configure,
            List<KeyValuePair<string, string>> formContent, CancellationToken cancellationToken = default)
            where T : class
        {
            using (var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Headers =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                },
                Content = new FormUrlEncodedContent(formContent)
            })
            {
                var result = await configure(httpRequest);

                if (result.HasErrors)
                {
                    var response = new ApiResponse<T>();
                    response.Merge(result);
                    return response;
                }

                return await PostAsync<T>(httpRequest, cancellationToken);
            }
        }

        private async Task<ApiResponse<T>> PostJsonAsync<T>(Func<HttpRequestMessage, Task<ApiResponse>> configure,
            object contentObj, CancellationToken cancellationToken = default) where T : class
        {
            var content = JsonConvert.SerializeObject(contentObj);
            using (var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Headers =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                },
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            })
            {
                var result = await configure(httpRequest);

                if (result.HasErrors)
                {
                    var response = new ApiResponse<T>();
                    response.Merge(result);
                    return response;
                }

                return await PostAsync<T>(httpRequest, cancellationToken);
            }
        }
        private async Task<ApiResponse<T>> GetJsonAsync<T>(Func<HttpRequestMessage, Task<ApiResponse>> configure, CancellationToken cancellationToken = default) where T : class
        {
            using (var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                },
            })
            {
                var result = await configure(httpRequest);

                if (result.HasErrors)
                {
                    var response = new ApiResponse<T>();
                    response.Merge(result);
                    return response;
                }

                return await PostAsync<T>(httpRequest, cancellationToken);
            }
        }

        private async Task<ApiResponse> EnrichRequestWithAccessTokenAsync(HttpRequestMessage message,
            CancellationToken cancellationToken = default)
        {
            var result = new ApiResponse();
            var tokenResponse = await GetAccessTokenAsync(cancellationToken);

            if (tokenResponse.IsSuccess && tokenResponse.Data != null)
            {
                message.Headers.Add("Cookie", $"wp_logged_in_cookie={tokenResponse.Data.Cookie}");
            }
            else
            {
                result.Merge(tokenResponse);
            }

            return result;
        }

        private async Task<ApiResponse<T>> PostAsync<T>(HttpRequestMessage message,
            CancellationToken cancellationToken = default) where T : class
        {
            var result = new ApiResponse<T>();

            var response = await _httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var stringContent = System.Text.Encoding.UTF8.GetString(byteArray);

            try
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    _logger.LogError($"API error: {stringContent}");
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        result.Data = JsonConvert.DeserializeObject<T>(stringContent);
                        break;
                    case HttpStatusCode.Unauthorized:
                        result.AddError("Unauthorized");
                        break;
                    case HttpStatusCode.BadRequest:
                        result.AddError("BadRequest");
                        break;
                    case HttpStatusCode.InternalServerError:
                        result.AddError("InternalServerError");
                        break;
                    case HttpStatusCode.NotFound:
                        result.AddError("NotFound");
                        break;
                    default:
                        result.AddError("UnknownError");
                        break;
                }

                if (!response.IsSuccessStatusCode && result.IsSuccess)
                {
                    result.AddError("UnknownError");
                }

                return result;
            }
            catch (JsonSerializationException ex)
            {
                result.AddError($"Unable to deserialize response; url: {message.RequestUri}; response: {stringContent}");
                _logger.LogError(
                    $"Error while processing response; url: ${message.RequestUri}; response: {stringContent}",
                    ex);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error while processing response; url: ${message.RequestUri}; response: {stringContent}",
                    ex);
                throw;
            }
        }

        #endregion

    }
}
