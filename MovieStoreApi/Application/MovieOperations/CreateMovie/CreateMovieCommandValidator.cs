using System;
using FluentValidation;

namespace MovieStoreApi.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommandValidator :AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty();
            RuleFor(c => c.Model.DirectorId).GreaterThan(0);
            RuleFor(c => c.Model.GenreId).GreaterThan(0);
            RuleFor(c => c.Model.Year).GreaterThan(1900).LessThan(DateTime.Now.Year + 1);
            RuleFor(c => c.Model.Price).GreaterThan(0);
        }
    }
}

