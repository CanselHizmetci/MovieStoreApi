using System;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly MovieStoreDbContext _context;
        public DeleteMovieCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var movie = _context.Movies.FirstOrDefault(c => c.Id == MovieId && !c.IsDeleted);
            if (movie == null)
                throw new InvalidOperationException("Silmek istediğiniz film mevcut değil");

            movie.IsDeleted = true;
            //_context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}

