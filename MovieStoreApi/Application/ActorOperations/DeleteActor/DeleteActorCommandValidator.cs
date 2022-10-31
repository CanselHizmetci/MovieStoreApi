using System;
using FluentValidation;

namespace MovieStoreApi.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandValidator:AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(c => c.ActorId).GreaterThan(0);
        }
    }
}

