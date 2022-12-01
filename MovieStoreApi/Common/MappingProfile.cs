using System;
using AutoMapper;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.Application.ActorOperations.GetActors;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.Application.DirectorOperations.GetDirectors;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.Application.MovieOperations.GetMovies;
using MovieStoreApi.Application.MovieOperations.UpdateMovie;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.Application.OrderOperations.GetOrders;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Common
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<int,Actor>().ForMember(c => c.Id , c=> c.MapFrom(c=> c));
            CreateMap<int,Movie>().ForMember(c => c.Id , c=> c.MapFrom(c=> c));
            CreateMap<CreateMovieModel, Movie>().ForMember(c=> c.Actors, c=>c.MapFrom(c=> c.Actors));
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, OrderViewModel>().ForMember(c=> c.CustomerName, c=> c.MapFrom(c=> c.Customer.Name + " " + c.Customer.Surname)).ForMember(c => c.PurchasedMovie, c => c.MapFrom(c => c.PurchasedMovie.Name));
            CreateMap<CreateActorModel, Actor>();
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Movie, MovieViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + "" + c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.Surname).ToList()));
            CreateMap<Director, DirectorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Actor, ActorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Actor, ActorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Director, DirectorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Movie, MoviesViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name +""+ c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " "+c.Surname).ToList()));

        }
    }
}

