using System;
using AutoMapper;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.OrderOperations.CreateOrder
{
	public class CreateOrderCommand
	{
        public CreateOrderModel Model { get; set; }
		private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateOrderCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task Handle()
        {
            var order = _mapper.Map<Order>(Model);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
	}
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}

