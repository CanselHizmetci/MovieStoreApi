using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQuery
    {
        public MovieViewModel Model { get; set; }
        public int MovieId { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MovieViewModel> Handle()
        {
            var movie = _context.Movies.Where(c=> !c.IsDeleted).Include(c=> c.Genre).Include(c => c.Director).Include(c => c.Actors).FirstOrDefault(c => c.Id == MovieId);
            if (movie == null)
                throw new InvalidOperationException("Film mevcut değil");

            Model = _mapper.Map<MovieViewModel>(movie);
            return Model;
        }

    }
    public class MovieViewModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public ICollection<string> Actors { get; set; }
        public decimal Price { get; set; }
    }
}

