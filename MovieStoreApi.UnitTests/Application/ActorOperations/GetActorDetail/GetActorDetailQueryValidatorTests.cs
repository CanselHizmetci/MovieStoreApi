using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.DbOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActorDetail
{
    public class GetActorDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = 0;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actor.Id;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);

        }
    }
}

