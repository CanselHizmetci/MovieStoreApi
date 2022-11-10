using System;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.UpdateDirector;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.UpdateDirector
{
    public class UpdateDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
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

            _context.Directors.Remove(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = director.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            var director = new Entities.Director
            {
                Name = "updateTest",
                Surname = "updateTest"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);

            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyTest"
            };
            command.DirectorId = director.Id;
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            director=_context.Directors.FirstOrDefault(c=> c.Id== command.DirectorId);
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);

        }
    }
}

