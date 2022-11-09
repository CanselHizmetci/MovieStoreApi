using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.GetDirectors;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.GetDirectors
{
    public class GetDirectorQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        public void WhenQueryGetResult_Director_ShouldNotBeReturnErrors()
        {
            GetDirectorQuery query = new GetDirectorQuery(_context, _mapper);
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

