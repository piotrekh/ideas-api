using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Categories.QueryHandlers;
using Ideas.UnitTests.TestDoubles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Categories
{
    public class GetCategoriesTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCategoriesTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Categories.GetCategoriesHandler_Should_ReturnAllCategories_When_CategoriesExist")]
        public void GetCategoriesHandler_Should_ReturnAllCategories_When_CategoriesExist()
        {
            #region Arrange

            var categoriesEntities = new List<IdeaCategory>()
            {
                new IdeaCategory() { Id = 1, Name = "Category1", CreatedDate = DateTime.UtcNow },
                new IdeaCategory() { Id = 2, Name = "Category2", CreatedDate = DateTime.UtcNow },
                new IdeaCategory() { Id = 3, Name = "Category3", CreatedDate = DateTime.UtcNow }
            };
            _uow.IdeaCategories.AddRange(categoriesEntities);
            _uow.SaveChanges();

            var handler = new GetCategoriesHandler(_uow, _mapper);

            #endregion

            #region Act

            var categoriesList = handler.Handle(new GetCategories());

            #endregion

            #region Assert

            categoriesList.ShouldNotBeNull();
            categoriesList.Items.ShouldNotBeEmpty();
            categoriesList.Items.Count.ShouldBe(categoriesEntities.Count);
            categoriesList.Items.ForEach(x => categoriesEntities.FirstOrDefault(y => y.Id.ToString() == x.Id).ShouldNotBeNull());

            #endregion
        }

        [Fact(DisplayName = "Categories.GetCategoriesHandler_Should_ReturnEmptyList_When_NoCategoriesExist")]
        public void GetCategoriesHandler_Should_ReturnEmptyList_When_NoCategoriesExist()
        {
            #region Arrange
            
            var handler = new GetCategoriesHandler(_uow, _mapper);

            #endregion

            #region Act

            var categoriesList = handler.Handle(new GetCategories());

            #endregion

            #region Assert

            categoriesList.ShouldNotBeNull();
            categoriesList.Items.ShouldNotBeNull();
            categoriesList.Items.ShouldBeEmpty();

            #endregion
        }
    }
}
