using System;
using FluentValidation;

namespace MovieStoreApi.Application.ActorOperations.UpdateActor
{
    public class UpdateActorCommandValidator:AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(c => c.ActorId).GreaterThan(0);
        }
    }
}

