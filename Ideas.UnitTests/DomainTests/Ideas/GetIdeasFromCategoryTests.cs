using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Ideas.Queries;
using Ideas.Domain.Ideas.QueryHandlers;
using Ideas.UnitTests.TestDoubles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Ideas
{
    public class GetIdeasFromCategoryTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetIdeasFromCategoryTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromCategoryHandler_Should_ReturnIdeas_When_CategoryExistsAndHasIdeas")]
        public void GetIdeasFromCategoryHandler_Should_ReturnIdeas_When_CategoryExistsAndHasIdeas()
        {
            #region Arrange

            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category", CreatedDate = DateTime.UtcNow };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var ideasEntities = new List<Idea>()
            {
                new Idea() { Id = 1, IdeaCategoryId = categoryEntity.Id, Title = "aaa", CreatedDate = DateTime.UtcNow },
                new Idea() { Id = 2, IdeaCategoryId = categoryEntity.Id, Title = "bbb", CreatedDate = DateTime.UtcNow },
                new Idea() { Id = 3, IdeaCategoryId = categoryEntity.Id, Title = "ccc", CreatedDate = DateTime.UtcNow }
            };
            _uow.Ideas.AddRange(ideasEntities);
            _uow.SaveChanges();

            var query = new GetIdeasFromCategory() { CategoryId = categoryEntity.Id.ToString() };
            var handler = new GetIdeasFromCategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            var result = handler.Handle(query);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeEmpty();
            result.Items.Count.ShouldBe(ideasEntities.Count);
            foreach(var ideaEntity in ideasEntities)
            {
                var idea = result.Items.FirstOrDefault(x => x.Id == ideaEntity.Id.ToString());
                idea.ShouldNotBeNull();
                idea.Title.ShouldBe(ideaEntity.Title);
            }

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromCategoryHandler_Should_ReturnEmptyIdeas_When_CategoryExistsButHasNoIdeas")]
        public void GetIdeasFromCategoryHandler_Should_ReturnEmptyIdeas_When_CategoryExistsButHasNoIdeas()
        {
            #region Arrange

            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category", CreatedDate = DateTime.UtcNow };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var query = new GetIdeasFromCategory() { CategoryId = categoryEntity.Id.ToString() };
            var handler = new GetIdeasFromCategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            var result = handler.Handle(query);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.ShouldBeEmpty();

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromCategoryHandler_Should_ThrowException_When_CategoryDoesNotExist")]
        public void GetIdeasFromCategoryHandler_Should_ThrowException_When_CategoryDoesNotExist()
        {
            #region Arrange

            var query = new GetIdeasFromCategory() { CategoryId = "123" };
            var handler = new GetIdeasFromCategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(query));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<InvalidCategoryIdException>();

            #endregion
        }
    }
}
