using System;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.DeleteDirector;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = 0;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeDirectorValidator",
                Surname = "ForHappyCodeDirectorValidator"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            var directorId = director.Id;

            _context.Remove(director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = directorId;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}

