using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Common;
using MovieStoreApi.DbOperations;

namespace MovieStoreApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName:"MovieStoreTestDb").Options;
            Context = new MovieStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddActors();

            Mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
        }
    }
}

