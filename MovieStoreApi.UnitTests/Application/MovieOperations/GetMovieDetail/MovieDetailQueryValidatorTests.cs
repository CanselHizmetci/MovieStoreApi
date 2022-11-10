using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
        {
            MovieDetailQuery query = new MovieDetailQuery(_context, _mapper);
            query.MovieId = 0;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Surname = "ForHappyCodeMovieDetailQueryTests"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeMovieDetailQueryTests"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieId = movie.Id;
            MovieDetailQuery query = new MovieDetailQuery(_context, _mapper);
            query.MovieId = movieId;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}

