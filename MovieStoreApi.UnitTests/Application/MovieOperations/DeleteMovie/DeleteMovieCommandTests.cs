using System;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.DeleteMovie;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieId = movie.Id;

            _context.Remove(movie);
            _context.SaveChanges();

            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = movieId;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz film mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteMovie_ShouldNotBeReturnError()
        {

        }
    }
}

