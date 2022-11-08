using System;
using AutoMapper;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _context;
        private IMovieStoreDbContext context;

        public DeleteActorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task Handle()
        {
            var actor = _context.Actors.FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Silmek istediğiniz aktör mevcut değil");

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }
    }
}

