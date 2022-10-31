using System;
using AutoMapper;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.ActorOperations.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateActorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle ()
        {
            var actor = _context.Actors.FirstOrDefault(c => c.Name == Model.Name && c.Surname == Model.Surname);
            if (actor is not null)
                throw new InvalidOperationException("Eklemek istediğiniz aktör zaten var");

            actor = _mapper.Map<Actor>(Model);
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }
    }

    public class CreateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

