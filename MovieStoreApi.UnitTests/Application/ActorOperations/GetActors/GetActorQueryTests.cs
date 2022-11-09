using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActors;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActors
{
    public class GetActorQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Movie_ShouldNotBeReturnErrors()
        {
            GetActorQuery query = new GetActorQuery(_context, _mapper);
            FluentActions.Invoking(()=> query.Handle().GetAwaiter().GetResult());
        }
    }
}

