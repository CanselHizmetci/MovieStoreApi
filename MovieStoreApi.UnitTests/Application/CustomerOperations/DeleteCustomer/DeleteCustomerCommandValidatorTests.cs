using System;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.DeleteCustomer;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            var customer = new Entities.Customer
            {
                Name = "WhenActorIdLessThanZero_Validator_ShouldBeReturnError",
                Surname = "WhenActorIdLessThanZero_Validator_ShouldBeReturnError"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = 0;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {

            var customer = new Entities.Customer
            {
                Name = "ForHappyCodeCustomerDeleteValidator",
                Surname = "ForHappyCodeCustomerDeleteValidator"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}

