using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.OrderOperations.CreateOrder
{
	public class CreateOrderCommandTests:IClassFixture<CommonTestFixture>
	{
		private readonly MovieStoreDbContext _context;
		private readonly IMapper _mapper;
		public CreateOrderCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}
		[Fact]
		public void WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn()
		{
			var customer = new Entities.Customer
			{
				Name = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn",
				Surname = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn"
			};
            var director = new Entities.Director
            {
                Name = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            var order = new Entities.Order
            {
                CustomerId = customer.Id,
                MovieId = movie.Id
            };

            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            //command.Model = new CreateOrderModel { PurchasedMovie = order.PurchasedMovie, Customer = order.Customer };
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu sipariş mevcut");
		}

    }
}

