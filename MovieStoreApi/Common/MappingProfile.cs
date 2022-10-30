using System;
using AutoMapper;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.Application.MovieOperations.GetMovies;
using MovieStoreApi.Application.MovieOperations.UpdateMovie;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Common
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<int,Actor>().ForMember(c => c.Id , c=> c.MapFrom(c=> c));
            CreateMap<CreateMovieModel, Movie>().ForMember(c=> c.Actors, c=>c.MapFrom(c=> c.Actors));
            CreateMap<Movie, MovieViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + "" + c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.Surname).ToList()));
            CreateMap<Movie, MoviesViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name +""+ c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " "+c.Surname).ToList()));

        }
    }
}

