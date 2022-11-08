using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.UpdateActor;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.UpdateActor
{
    public class UpdateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
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

            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = actor.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz aktör bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            //Arrange
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context);
            UpdateActorModel model = new UpdateActorModel()
            {
                Name = "ForHappyCodeTest",
                Surname = "ForHappyCodeTest"
            };

            command.Model = model;
            command.ActorId = actor.Id;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            actor = _context.Actors.FirstOrDefault(c => c.Id == command.ActorId);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.Name);
            actor.Surname.Should().Be(model.Surname);

        }
    }
}

