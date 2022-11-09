using System;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.DeleteActor;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var actor = new Actor()
            {
                Name = "ForNotExistActor",
                Surname = "ForNotExistActor"
            };
           _context.Actors.Add(actor);
            _context.SaveChanges();

            var actorId = actor.Id;
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
              Name="ForHappyCode",
              Surname="ForHappyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actor.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}

