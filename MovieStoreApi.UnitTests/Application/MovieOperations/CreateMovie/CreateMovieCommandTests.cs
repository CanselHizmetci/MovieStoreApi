using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn",
                Year = 2000,
                Price = 90,
                DirectorId = director.Id,
                GenreId=genre.Id
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel() { Name = movie.Name, DirectorId = movie.DirectorId, GenreId = movie.GenreId, Price = movie.Price, Year = movie.Year };
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz film zaten mevcut");
            
        }
    }
}

