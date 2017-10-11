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
    public class GetIdeasFromSubcategoryTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetIdeasFromSubcategoryTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromSubcategoryHandler_Should_ReturnIdeas_When_SubcategoryExistsAndHasIdeas")]
        public void GetIdeasFromSubcategoryHandler_Should_ReturnIdeas_When_SubcategoryExistsAndHasIdeas()
        {
            #region Arrange

            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var subcategoryEntity = new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcategory" };
            _uow.IdeaSubcategories.Add(subcategoryEntity);
            _uow.SaveChanges();
            
            var ideasEntities = new List<Idea>()
            {
                new Idea() { Id = 1, IdeaCategoryId = categoryEntity.Id, Title = "aaa" },
                new Idea() { Id = 2, IdeaCategoryId = categoryEntity.Id, Title = "bbb" },
                new Idea() { Id = 3, IdeaCategoryId = categoryEntity.Id, Title = "ccc" }
            };
            _uow.Ideas.AddRange(ideasEntities);
            _uow.SaveChanges();

            var assignedSubcategories = new List<AssignedIdeaSubcategory>();
            foreach (var ideaEntity in ideasEntities)
                assignedSubcategories.Add(new AssignedIdeaSubcategory() { IdeaId = ideaEntity.Id, IdeaSubcategoryId = subcategoryEntity.Id });
            _uow.AssignedIdeaSubcategories.AddRange(assignedSubcategories);
            _uow.SaveChanges();

            var query = new GetIdeasFromSubcategory() { CategoryId = categoryEntity.Id.ToString(), SubcategoryId = subcategoryEntity.Id.ToString() };
            var handler = new GetIdeasFromSubcategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            var result = handler.Handle(query);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeEmpty();
            result.Items.Count.ShouldBe(ideasEntities.Count);
            foreach (var ideaEntity in ideasEntities)
            {
                var idea = result.Items.FirstOrDefault(x => x.Id == ideaEntity.Id.ToString());
                idea.ShouldNotBeNull();
                idea.Title.ShouldBe(ideaEntity.Title);
            }

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromSubcategoryHandler_Should_ReturnEmptyIdeas_When_SubcategoryExistsButHasNoIdeas")]
        public void GetIdeasFromSubcategoryHandler_Should_ReturnEmptyIdeas_When_SubcategoryExistsButHasNoIdeas()
        {
            #region Arrange

            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var subcategoryEntity = new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcategory" };
            _uow.IdeaSubcategories.Add(subcategoryEntity);
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

        [Fact(DisplayName = "Ideas.GetIdeasFromSubcategoryHandler_Should_ThrowException_When_SubcategoryIsNotAssignedToCategory")]
        public void GetIdeasFromSubcategoryHandler_Should_ThrowException_When_SubcategoryIsNotAssignedToCategory()
        {
            #region Arrange

            var categoriesEntities = new List<IdeaCategory>()
            {
                new IdeaCategory() { Id = 1, Name = "category_1" },
                new IdeaCategory() { Id = 2, Name = "category_2" }
            };
            _uow.IdeaCategories.AddRange(categoriesEntities);
            _uow.SaveChanges();

            var subcategoryEntity = new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoriesEntities[1].Id, Name = "subcategory" };
            _uow.IdeaSubcategories.Add(subcategoryEntity);
            _uow.SaveChanges();

            var query = new GetIdeasFromSubcategory() { CategoryId = categoriesEntities[0].Id.ToString(), SubcategoryId = subcategoryEntity.Id.ToString() };
            var handler = new GetIdeasFromSubcategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(query));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<InvalidSubcategoryIdException>();

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromSubcategoryHandler_Should_ThrowException_When_SubcategoryDoesNotExist")]
        public void GetIdeasFromSubcategoryHandler_Should_ThrowException_When_SubcategoryDoesNotExist()
        {
            #region Arrange

            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var query = new GetIdeasFromSubcategory() { CategoryId = categoryEntity.Id.ToString(), SubcategoryId = "123" };
            var handler = new GetIdeasFromSubcategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(query));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<SubcategoryNotFoundException>();

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeasFromSubcategoryHandler_Should_ThrowException_When_CategoryDoesNotExist")]
        public void GetIdeasFromSubcategoryHandler_Should_ThrowException_When_CategoryDoesNotExist()
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
