using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.OrderOperations.CreateOrder
{
    public class CreateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        public static readonly object[][] CorrectData =
        {
        new object[] {(0,1,200, new DateTime(2000, 4, 20))},
        new object[] {(1, 0, 200, new DateTime(2000, 5, 20)) },
        new object[] {(1, 1, 0, new DateTime(2000, 6, 20)) },
        new object[] {(1, 1, 200,"")}
        };
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(int customerId, int movieId, decimal price, DateTime purchasedDate)
        {
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel
            {
                CustomerId=customerId,
                MovieId=movieId,
                Price=price,
                PurchasedDate=purchasedDate
            };
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
