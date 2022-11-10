using System;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.DeleteCustomer;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        public void WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var customer = new Entities.Customer
            {
                Name = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            _context.Remove(customer);
            _context.SaveChanges();

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        public void WhenValidInputsAreGiven_DeleteCustomer_ShouldNotBeReturnError()
        {
            var customer = new Entities.Customer
            {
                Name = "ForHappyCodeCustomer",
                Surname = "ForHappyCodeCustomer"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

