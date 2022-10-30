using System;
using AutoMapper;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommand()
        {
        }
    }
}

