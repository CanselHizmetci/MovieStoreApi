using System;
using FluentValidation;

namespace MovieStoreApi.Application.ActorOperations.GetActorDetail
{
    public class ActorDetailQueryValidator:AbstractValidator<ActorDetailQuery>
    {
        public ActorDetailQueryValidator()
        {
            RuleFor(c => c.ActorId).GreaterThan(0);
        }
    }
}

