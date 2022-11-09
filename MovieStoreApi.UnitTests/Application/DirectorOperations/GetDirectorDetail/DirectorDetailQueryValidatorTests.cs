using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.DbOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DirectorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            DirectorDetailQuery query = new DirectorDetailQuery(_context, _mapper);
            query.DirectorId = 0;

            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
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

            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}

