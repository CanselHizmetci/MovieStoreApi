using System;
using System.Numerics;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.CreateActor
{
    public class CreateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistActor_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var actor = new Actor
            {
                Name = "WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistActorSurnameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            CreateActorCommand command = new (_context, _mapper);
            command.Model = new CreateActorModel() { Name = actor.Name, Surname = actor.Surname };

            //Act & Assert (Çalıştırma-Doğrulama)
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz aktör zaten var");

        }

    }
}

