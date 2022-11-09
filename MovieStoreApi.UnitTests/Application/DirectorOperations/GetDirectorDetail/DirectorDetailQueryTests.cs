using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            var directorId = director.Id;

            _context.Remove(director);
            _context.SaveChanges();

            DirectorDetailQuery query = new DirectorDetailQuery(_context, _mapper);
            query.DirectorId = directorId;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen mevcut değil");

        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_Actor_ShouldNotBeReturnErrors()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            var directorId = director.Id;

            DirectorDetailQuery query = new DirectorDetailQuery(_context, _mapper);
            query.DirectorId = directorId;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

