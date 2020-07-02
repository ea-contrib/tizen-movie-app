using System;
using System.Collections.Generic;
using AutoMapper;
using TMA.Contracts.Constants;
using TMA.Contracts.Dto;
using TMA.Contracts.Enums;
using TMA.MovieService.Clients.PuzzleMovies;
using TMA.MovieService.Entities;

namespace TMA.MovieService.Mappings
{
    public class MovieProfile: Profile
    {
        private static readonly Dictionary<PuzzleMoviesMovieType, MovieType> PuzzleMovieTypeMap =
            new Dictionary<PuzzleMoviesMovieType, MovieType>()
            {
                {PuzzleMoviesMovieType.Cartoon, MovieType.Cartoon},
                {PuzzleMoviesMovieType.Film, MovieType.Film},
                {PuzzleMoviesMovieType.Series1, MovieType.Series},
                {PuzzleMoviesMovieType.Series2, MovieType.Series}
            };


        public MovieProfile()
        {
            CreateMap<PuzzleMoviesMovieModel, MovieEntity>()
                .ForMember(x => x.ExternalId, opt => opt.MapFrom(x => x.MovieId.ToString()))
                .ForMember(x => x.ExternalId2, opt => opt.MapFrom(x => x.Slug))
                .ForMember(x => x.ProviderId, opt => opt.MapFrom((x, _) => MovieProviders.PuzzleMovies))
                .ForMember(x => x.Type, opt => opt.MapFrom((x, _) => (int) (PuzzleMovieTypeMap.ContainsKey(x.Type) ? PuzzleMovieTypeMap[x.Type] : MovieType.None )))
                .ForMember(x => x.Country, opt => opt.MapFrom(x => x.Country))
                .ForMember(x => x.Popularity, opt => opt.MapFrom(x => x.Popularity))
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Rating))
                .ForMember(x => x.ReleaseYear, opt => opt.MapFrom(x => x.ReleaseYear))
                .ForMember(x => x.TitleRu, opt => opt.MapFrom(x => x.TitleRu))
                .ForMember(x => x.TitleEn, opt => opt.MapFrom(x => x.TitleEn))
                .ForMember(x => x.UpdateTime, opt => opt.MapFrom(x => DateTime.UtcNow))
                ;

            CreateMap<MovieEntity, MovieDto>();
        }
    }
}
