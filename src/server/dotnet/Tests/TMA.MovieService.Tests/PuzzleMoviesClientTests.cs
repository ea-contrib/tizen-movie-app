using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using TMA.MovieService.Clients;
using TMA.MovieService.Clients.PuzzleMovies;

namespace TMA.MovieService.Tests
{
    [TestFixture]
    public class PuzzleMoviesClientTests
    {
        public PuzzleMoviesClient GetClient()
        {
            return new PuzzleMoviesClient(new PuzzleMoviesClientConfigurationFactory(), Substitute.For<ILogger<PuzzleMoviesClient>>());
        }

        [Test]
        public async Task ListTest()
        {
            var client = GetClient();
            var result = await client.MoviesList();

            result.IsSuccess.Should().BeTrue();
            result.Data.Movies.Count.Should().BeGreaterThan(0);
        }
    }
}
