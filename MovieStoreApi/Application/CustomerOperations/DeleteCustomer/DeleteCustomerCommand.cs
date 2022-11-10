using System;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == CustomerId);
            if (customer == null)
                throw new InvalidOperationException("Silmek istediğiniz müşteri bulunamadı");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}

