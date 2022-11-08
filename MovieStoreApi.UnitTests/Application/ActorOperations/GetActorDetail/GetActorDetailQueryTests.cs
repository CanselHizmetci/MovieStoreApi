using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActorDetail
{
    public class GetActorDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        //Arrange
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var actor = new Actor()
            {
                Name = "ForNoExistActor",
                Surname = "ForNoExistActor"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            var actorId = actor.Id;
            _context.Actors.Remove(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör mevcut değil");
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_Actor_ShouldNotBeReturnErrors()
        {
            var actor = new Actor
            {
                Name = "ForHappyCode",
                Surname = "ForHapyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actor.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

