using System;
using System.Xml.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCustomerCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name,string surname)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            CreateCustomerModel model = new CreateCustomerModel
            {
                Name = name,
                Surname = surname
            };
            command.Model = model;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            CreateCustomerModel model = new CreateCustomerModel
            {
                Name = "ForHappyCodeCustomerValidator",
                Surname = "ForHappyCodeCustomerValidator"
            };
            command.Model = model;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);

        }


    }
}

