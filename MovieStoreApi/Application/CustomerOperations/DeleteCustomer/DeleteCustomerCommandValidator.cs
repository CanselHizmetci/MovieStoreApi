using System;
using FluentValidation;

namespace MovieStoreApi.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandValidator:AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
        }
    }
}

