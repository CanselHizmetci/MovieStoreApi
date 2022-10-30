using System;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Entities;

namespace MovieStoreApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                    return;

                context.Actors.AddRange(

                    new Actor
                    {
                        Name="Brad",
                        Surname="Pitt",
                        Id=1
                    },
                    new Actor
                    {
                        Name = "Johnny",
                        Surname = "Depp",
                        Id = 2
                    },
                    new Actor
                    {
                        Name = "Emma",
                        Surname = "Watson",
                        Id = 3
                    }
                    );
                context.Directors.AddRange(

                    new Director
                    {
                        Name= "David",
                        Surname= "Fincher",
                        Id=1
                    },
                    new Director
                    {
                        Name = "Wally",
                        Surname = "Pfister",
                        Id = 2
                    },
                    new Director
                    {
                        Name = "Chris",
                        Surname = "Columbus",
                        Id = 3
                    }
                    );
                context.Genres.AddRange(

                    new Genre
                    {
                        Name= "thriller"
                    },
                    new Genre
                    {
                        Name = "Science fiction"
                    },
                    new Genre
                    {
                        Name = "Fantastic"
                    }
                    );
                context.SaveChanges();
                context.Movies.AddRange(

                    new Movie
                    {
                        Name="Fight Club",
                        Year= 1999,
                        Actors= context.Actors.Where(c=> new[] {1,3}.Contains(c.Id)).ToList(),
                        DirectorId=1,
                        GenreId=1,
                        Price=40
                    },
                    new Movie
                    {
                        Name = "Transcendence",
                        Year = 2014,
                        Actors = context.Actors.Where(c => new[] { 2 }.Contains(c.Id)).ToList(),
                        DirectorId = 2,
                        GenreId = 2,
                        Price = 60
                    },
                    new Movie
                    {
                        Name = "Harry Potter",
                        Year = 1999,
                        Actors = context.Actors.Where(c => new[] { 1, 2, 3 }.Contains(c.Id)).ToList(),
                        DirectorId = 3,
                        GenreId = 3,
                        Price = 80
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}

