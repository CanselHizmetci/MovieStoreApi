using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle()
        {
            var movie = _context.Movies.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Name == Model.Name && c.DirectorId == Model.DirectorId);
            if (movie is not null)
                throw new InvalidOperationException("Eklemek istediğiniz film zaten mevcut");
            movie = _mapper.Map<Movie>(Model);
            _context.Movies.Add(movie);
            foreach (var a in movie.Actors)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }
    }

    public class CreateMovieModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public IEnumerable<int> Actors { get; set; }
        public decimal Price { get; set; }
    }
}

