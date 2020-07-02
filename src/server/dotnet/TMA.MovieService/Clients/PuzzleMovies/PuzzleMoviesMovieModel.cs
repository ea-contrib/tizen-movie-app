using System.Collections.Generic;
using Newtonsoft.Json;

namespace TMA.MovieService.Clients.PuzzleMovies
{
    public class PuzzleMoviesMovieModel
    {
        [JsonProperty("movie_id")]
        public int MovieId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("media_type")]
        public PuzzleMoviesMovieType Type { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        [JsonProperty("title_ru")]
        public string TitleRu { get; set; }

        [JsonProperty("release_year")]
        public string ReleaseYear { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("last_update")]
        public double LastUpdate { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("genres")]
        public List<PuzzleMoviesMovieGenre> Genres { get; set; }
    }
}
