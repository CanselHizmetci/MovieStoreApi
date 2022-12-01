using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.OrderOperations.GetOrders;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.OrderOperations.GetOrders
{
	public class GetOrderQueryTests:IClassFixture<CommonTestFixture>
	{
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Order_ShouldNotBeReturnErrors()
        {
            GetOrderQuery query = new GetOrderQuery(_context, _mapper);
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

