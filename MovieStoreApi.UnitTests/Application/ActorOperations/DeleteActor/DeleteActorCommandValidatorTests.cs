using System;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.DeleteActor;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 0;

            //Axt
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
    
}

