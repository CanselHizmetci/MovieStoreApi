using System;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
            context.Actors.AddRange(

                    new Actor
                    {
                        Name = "Brad",
                        Surname = "Pitt"
                    },
                    new Actor
                    {
                        Name = "Johnny",
                        Surname = "Depp"
                    },
                    new Actor
                    {
                        Name = "Emma",
                        Surname = "Watson"
                    }
                    );
        }
    }
}

