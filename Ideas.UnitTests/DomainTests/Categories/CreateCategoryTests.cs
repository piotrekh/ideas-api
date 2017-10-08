using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Categories.CommandHandlers;
using Ideas.Domain.Categories.Commands;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Categories.Models;
using Ideas.UnitTests.TestDoubles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Categories
{
    public class CreateCategoryTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateCategoryTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Categories.CreateCategoryHandler_Should_CreateCategory_When_NameIsNotTaken")]
        public void CreateCategoryHandler_Should_CreateCategory_When_NameIsNotTaken()
        {
            #region Arrange

            var command = new CreateCategory() { Name = "My category" };
            var commandHandler = new CreateCategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            Category result = commandHandler.Handle(command);

            #endregion

            #region Assert

            result.ShouldNotBeNull();
            result.Name.ShouldBe(command.Name);

            _uow.IdeaCategories.Count().ShouldBe(1);

            #endregion
        }

        [Fact(DisplayName = "Categories.CreateCategoryHandler_Should_RaiseException_When_NameIsTaken")]
        public void CreateCategoryHandler_Should_RaiseException_When_NameIsTaken()
        {
            #region Arrange

            string categoryName = "My category";

            //insert a category with the given name
            _uow.IdeaCategories.Add(new IdeaCategory()
            {
                Id = 1,
                CreatedDate = DateTime.UtcNow,
                Name = categoryName
            });
            _uow.SaveChanges();

            var command = new CreateCategory() { Name = categoryName };
            var commandHandler = new CreateCategoryHandler(_uow, _mapper);

            #endregion

            #region Act

            //try to insert category with name that is already taken
            Exception ex = Record.Exception(() => commandHandler.Handle(command));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<CategoryAlreadyExistsException>();

            _uow.IdeaCategories.Count().ShouldBe(1);

            #endregion
        }
    }
}
