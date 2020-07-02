using Newtonsoft.Json;

namespace TMA.MovieService.Clients.PuzzleMovies
{
    public class PuzzleMoviesTokenResponseModel
    {
        [JsonProperty("wp_logged_in_cookie")]
        public string Cookie { get; set; }
    }
}
