using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Categories.QueryHandlers;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Models;
using Ideas.UnitTests.TestDoubles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Categories
{
    public class GetSubcategoriesTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSubcategoriesTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Categories.GetSubcategoriesHandler_Should_ReturnSubcategories_When_CategoryExistsAndHasSubcategories")]
        public void GetSubcategoriesHandler_Should_ReturnSubcategories_When_CategoryExistsAndHasSubcategories()
        {
            #region Arrange

            //add category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "Category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            //add subcategories for the already added category
            var subcategoriesEntities = new List<IdeaSubcategory>()
            {
                new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcat_1" },
                new IdeaSubcategory() { Id = 2, IdeaCategoryId = categoryEntity.Id, Name = "subcat_2" },
                new IdeaSubcategory() { Id = 3, IdeaCategoryId = categoryEntity.Id, Name = "subcat_3" }
            };
            _uow.IdeaSubcategories.AddRange(subcategoriesEntities);
            _uow.SaveChanges();

            var query = new GetSubcategories() { CategoryId = categoryEntity.Id.ToString() };
            var handler = new GetSubcategoriesHandler(_uow, _mapper);

            #endregion

            #region Act

            ItemsResult<Subcategory> result = handler.Handle(query);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeEmpty();
            result.Items.Count.ShouldBe(subcategoriesEntities.Count);
            foreach(var subcategoryEntity in subcategoriesEntities)
            {
                var subcategory = result.Items.FirstOrDefault(x => x.Id == subcategoryEntity.Id.ToString());
                subcategory.ShouldNotBeNull();
                subcategoryEntity.Name.ShouldBe(subcategoryEntity.Name);
            }                

            #endregion
        }

        [Fact(DisplayName = "Categories.GetSubcategoriesHandler_Should_ReturnEmptySubcategories_When_CategoryExistsButHasNoSubcategories")]
        public void GetSubcategoriesHandler_Should_ReturnEmptySubcategories_When_CategoryExistsButHasNoSubcategories()
        {
            #region Arrange

            //add category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "Category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            var query = new GetSubcategories() { CategoryId = categoryEntity.Id.ToString() };
            var handler = new GetSubcategoriesHandler(_uow, _mapper);

            #endregion

            #region Act

            ItemsResult<Subcategory> result = handler.Handle(query);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.ShouldBeEmpty();

            #endregion
        }

        [Fact(DisplayName = "Categories.GetSubcategoriesHandler_Should_ThrowException_When_CategoryDoesNotExist")]
        public void GetSubcategoriesHandler_Should_ThrowException_When_CategoryDoesNotExist()
        {
            #region Arrange
            
            var query = new GetSubcategories() { CategoryId = "123" };
            var handler = new GetSubcategoriesHandler(_uow, _mapper);

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
