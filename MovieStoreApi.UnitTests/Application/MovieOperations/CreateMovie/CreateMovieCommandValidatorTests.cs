using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData("name", 2000, 1, 2, 0)]
        [InlineData("", 2000, 1, 2, 20)]
        [InlineData("name", 1800, 1, 2, 20)]
        [InlineData("name", 2000, 0, 2, 20)]
        [InlineData("name", 2000, 1, 0, 20)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, int year, int genreId, int directorId, decimal price)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel
            {
                Name = name,
                DirectorId = directorId,
                GenreId = genreId,
                Year = year,
                Price = price
            };
            command.Model = model;

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            var director = new Entities.Director
            {
                Name = "WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError",
                Surname = "WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "ForHappyCodeCreateValidator",
                Year = 2000,
                GenreId = genre.Id,
                DirectorId = director.Id,
                Price = 100
            };
            command.Model = model;
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}

