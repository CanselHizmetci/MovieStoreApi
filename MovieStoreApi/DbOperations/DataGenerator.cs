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
                        ActorName="Brad",
                        ActorSurname="Pitt",
                        MovieId=1
                    },
                    new Actor
                    {
                        ActorName = "Johnny",
                        ActorSurname = "Depp",
                        MovieId = 2
                    },
                    new Actor
                    {
                        ActorName = "Emma",
                        ActorSurname = "Watson",
                        MovieId = 3
                    }
                    );
                context.Directors.AddRange(

                    new Director
                    {
                        DirectorName= "David",
                        DirectorSurname= "Fincher",
                        MovieId=1
                    },
                    new Director
                    {
                        DirectorName = "Wally",
                        DirectorSurname = "Pfister",
                        MovieId = 2
                    },
                    new Director
                    {
                        DirectorName = "Chris",
                        DirectorSurname = "Columbus",
                        MovieId = 3
                    }
                    );
                context.Movies.AddRange(

                    new Movie
                    {
                        MovieName="Fight Club",
                        MovieYear= new DateTime(1999,12,10),
                        ActorId=1,
                        DirectorId=1,
                        GenreId=1,
                        Price=40
                    },
                    new Movie
                    {
                        MovieName = "Transcendence",
                        MovieYear = new DateTime(2014,4,10),
                        ActorId = 2,
                        DirectorId = 2,
                        GenreId = 2,
                        Price = 60
                    },
                    new Movie
                    {
                        MovieName = "Harry Potter",
                        MovieYear = new DateTime(1999, 12, 10),
                        ActorId = 3,
                        DirectorId = 3,
                        GenreId = 3,
                        Price = 80
                    }
                    );
                context.Genres.AddRange(

                    new Genre
                    {
                        GenreName= "thriller"
                    },
                    new Genre
                    {
                        GenreName = "Science fiction"
                    },
                    new Genre
                    {
                        GenreName = "Fantastic"
                    }
                    );
            }
        }
    }
}

