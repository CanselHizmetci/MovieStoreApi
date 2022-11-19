using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.Application.OrderOperations.GetOrders;
using MovieStoreApi.DbOperations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(MovieStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetOrderQuery query = new GetOrderQuery(_context, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateOrderModel newOrder)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = newOrder;
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

    }
}

