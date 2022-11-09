using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var director = _context.Directors.Include(c=>c.Movies).FirstOrDefault(c => c.Id == DirectorId);
            if (director == null)
                throw new InvalidOperationException("Silmek istediğiniz yönetmen mevcut değil");

            if (director.Movies.Any())
                throw new InvalidOperationException("Silmek istediğiniz yönetmenin filmi mevcut");

            _context.Remove(director);
            await _context.SaveChangesAsync();
        }
    }
}

