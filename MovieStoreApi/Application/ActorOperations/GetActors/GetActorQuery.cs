using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.Application.ActorOperations.GetActors
{
    public class GetActorQuery
    {
        public ActorsViewModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ActorsViewModel>> Handle()
        {
            var actorList = _context.Actors.Include(c=> c.Movies).OrderBy(c => c.Id).ToList();

            List<ActorsViewModel> vm = _mapper.Map<List<ActorsViewModel>>(actorList);
            return vm;
        }
    }

    public class ActorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}

