using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string surname)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = name,
                Surname = surname
            };
            command.Model = model;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "test",
                Surname = "test"
            };
            command.Model = model;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}

