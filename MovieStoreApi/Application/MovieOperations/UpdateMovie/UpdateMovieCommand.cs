using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }
        private readonly MovieStoreDbContext _context;
        public UpdateMovieCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task Handle()
        {
            var movie = _context.Movies.Where(c => !c.IsDeleted).Include(c=> c.Genre).Include(c=> c.Actors).FirstOrDefault(c => c.Id == MovieId);
            if (movie == null)
                throw new InvalidOperationException("Güncellemek istediğiniz film mevcut değil");


            movie.Price = Model.Price != default ? Model.Price : movie.Price;
            movie.Name = Model.Name != default ? Model.Name : movie.Name;
            movie.Year = Model.Year != default ? Model.Year : movie.Year;
            movie.GenreId = Model.GenreId != default ? Model.GenreId : movie.GenreId;
            movie.DirectorId = Model.DirectorId != default ? Model.DirectorId : movie.DirectorId;
            movie.Actors.Clear();
            movie.Actors = _context.Actors.Where(c => Model.Actors.Contains(c.Id)).ToList();
            foreach (var a in movie.Actors)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
            

        }
    }

    public class UpdateMovieModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<int> Actors { get; set; }
    }
}

