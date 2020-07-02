using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMA.Contracts.Constants;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MovieService.Clients.PuzzleMovies;
using TMA.MovieService.Entities;
using TMA.MovieService.Repositories;

namespace TMA.MovieService.Logic
{
    public class MovieBlo
    {
        private readonly MovieRepository _repository;
        private readonly PuzzleMoviesClient _puzzleMoviesClient;
        private readonly IMapper _mapper;

        public MovieBlo(MovieRepository repository, PuzzleMoviesClient puzzleMoviesClient, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _puzzleMoviesClient = puzzleMoviesClient ?? throw new ArgumentNullException(nameof(puzzleMoviesClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseMessage> SynchronizePuzzleMovies()
        {
            var movies = await _puzzleMoviesClient.MoviesList();

            var entities = await _repository.List(MovieProviders.PuzzleMovies);

            foreach (var puzzleMoviesMovieModel in movies.Data.Movies)
            {
                var externalId = puzzleMoviesMovieModel.MovieId.ToString();
                var existingEntity = entities.FirstOrDefault(x => x.ExternalId == externalId);

                if (existingEntity == null)
                {
                    var entity = _mapper.Map<MovieEntity>(puzzleMoviesMovieModel);

                    await _repository.Insert(entity);
                }
                else
                {
                     _mapper.Map(puzzleMoviesMovieModel, existingEntity);

                    await _repository.Update(existingEntity);
                }

            }


            return new ResponseMessage();
        }

        public async Task<ResponseMessage<List<MovieDto>>> List()
        {
            var documents = await _repository.List(null);

            return new ResponseMessage<List<MovieDto>>(documents.Select(_mapper.Map<MovieDto>).ToList());
        }
    }
}
