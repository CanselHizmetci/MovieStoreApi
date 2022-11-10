using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Entities.Genre
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            var customer = new Entities.Customer
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel() { Name = customer.Name, Surname = customer.Surname };

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz müşteri zaten var");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "ForHappyCodeCustomer",
                Surname = "ForHappyCodeCustomer"
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            var customer = _context.Customers.SingleOrDefault(c => c.Name == model.Name && c.Surname == model.Surname);
            customer.Should().NotBeNull();
        }

    }
}

