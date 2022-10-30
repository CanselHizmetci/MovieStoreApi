using System;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var director = _context.Directors.FirstOrDefault(c => c.Id == DirectorId);
            if (director == null)
                throw new InvalidOperationException("Yönetmen bulunamadı");

            director.Name = Model.Name != default ? Model.Name : director.Name;
            director.Surname = Model.Surname != default ? Model.Surname : director.Surname;
           
            await _context.SaveChangesAsync();
        }
    }

    public class UpdateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

