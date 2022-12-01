using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.OrderOperations.GetOrders
{
	public class GetOrderQuery
	{
		private readonly MovieStoreDbContext _context;
		private readonly IMapper _mapper;
		public GetOrderQuery(MovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<OrderViewModel>> Handle()
		{
			var orderList = await _context.Orders.Include(c=> c.PurchasedMovie).Include(c=> c.Customer).OrderBy(c => c.Id).ToListAsync();
			if (orderList == null)
				throw new InvalidOperationException("Sipariş listesi mevcut değil.");

			List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(orderList);
			return vm;
        }
	}
	public class OrderViewModel
	{
        public string CustomerName { get; set; }
        public string PurchasedMovie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}

