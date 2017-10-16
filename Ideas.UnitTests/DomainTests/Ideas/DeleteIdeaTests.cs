using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Common.Context;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Ideas.CommandHandlers;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Exceptions;
using Ideas.Domain.Users.Exceptions;
using Ideas.UnitTests.TestDoubles;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Ideas
{
    public class DeleteIdeaTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteIdeaTests()
        {
            _uow = new InMemoryUnitOfWork();
        }

        [Fact(DisplayName = "Ideas.DeleteIdeaHandler_Should_DeleteIdea_When_IdeaExistsAndIsBeingDeletedByAuthor")]
        public void DeleteIdeaHandler_Should_DeleteIdea_When_IdeaExistsAndIsBeingDeletedByAuthor()
        {
            #region Arrange
            
            //insert category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            //insert subcategories bound to the category
            var subcategoryEntities = new List<IdeaSubcategory>()
            {
                new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcategory1" },
                new IdeaSubcategory() { Id = 2, IdeaCategoryId = categoryEntity.Id, Name = "subcategory2" }
            };
            _uow.IdeaSubcategories.AddRange(subcategoryEntities);
            _uow.SaveChanges();

            //insert idea
            var ideaEntity = new Idea()
            {
                Id = 1,
                IdeaCategoryId = categoryEntity.Id,
                Title = "Sample title",
                AspNetUserId = 1,
                User = new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                CreatedDate = new DateTime(2017, 10, 16, 8, 34, 40),
                Description = "Sample description"
            };
            _uow.Ideas.Add(ideaEntity);
            _uow.SaveChanges();

            //assign subcategories to idea
            var assignedSubcategories = new List<AssignedIdeaSubcategory>();
            foreach (var subcategoryEntity in subcategoryEntities)
                assignedSubcategories.Add(new AssignedIdeaSubcategory() { IdeaId = ideaEntity.Id, IdeaSubcategoryId = subcategoryEntity.Id });
            _uow.AssignedIdeaSubcategories.AddRange(assignedSubcategories);
            _uow.SaveChanges();

            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.SetupGet(x => x.Id)
                .Returns(ideaEntity.User.Id);
            userContextMock.Setup(x => x.IsInRole(RoleName.Admin))
                .Returns(false);

            var command = new DeleteIdea() { IdeaId = ideaEntity.Id.ToString() };
            var handler = new DeleteIdeaHandler(_uow, userContextMock.Object);

            #endregion

            #region Act

            handler.Handle(command);

            #endregion

            #region Assert

            _uow.AssignedIdeaSubcategories.Any().ShouldBeFalse();
            _uow.Ideas.Any(x => x.Id == ideaEntity.Id).ShouldBeFalse();
            userContextMock.VerifyGet(x => x.Id, Times.Once);

            #endregion
        }

        [Fact(DisplayName = "Ideas.DeleteIdeaHandler_Should_DeleteIdea_When_IdeaExistsAndIsBeingDeletedByAdmin")]
        public void DeleteIdeaHandler_Should_DeleteIdea_When_IdeaExistsAndIsBeingDeletedByAdmin()
        {
            #region Arrange

            //insert category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            //insert subcategories bound to the category
            var subcategoryEntities = new List<IdeaSubcategory>()
            {
                new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcategory1" },
                new IdeaSubcategory() { Id = 2, IdeaCategoryId = categoryEntity.Id, Name = "subcategory2" }
            };
            _uow.IdeaSubcategories.AddRange(subcategoryEntities);
            _uow.SaveChanges();

            //insert idea
            var ideaEntity = new Idea()
            {
                Id = 1,
                IdeaCategoryId = categoryEntity.Id,
                Title = "Sample title",
                AspNetUserId = 1,
                User = new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                CreatedDate = new DateTime(2017, 10, 16, 8, 34, 40),
                Description = "Sample description"
            };
            _uow.Ideas.Add(ideaEntity);
            _uow.SaveChanges();

            //assign subcategories to idea
            var assignedSubcategories = new List<AssignedIdeaSubcategory>();
            foreach (var subcategoryEntity in subcategoryEntities)
                assignedSubcategories.Add(new AssignedIdeaSubcategory() { IdeaId = ideaEntity.Id, IdeaSubcategoryId = subcategoryEntity.Id });
            _uow.AssignedIdeaSubcategories.AddRange(assignedSubcategories);
            _uow.SaveChanges();

            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.SetupGet(x => x.Id)
                .Returns(123);
            userContextMock.Setup(x => x.IsInRole(RoleName.Admin))
                .Returns(true);

            var command = new DeleteIdea() { IdeaId = ideaEntity.Id.ToString() };
            var handler = new DeleteIdeaHandler(_uow, userContextMock.Object);

            #endregion

            #region Act

            handler.Handle(command);

            #endregion

            #region Assert

            _uow.AssignedIdeaSubcategories.Any().ShouldBeFalse();
            _uow.Ideas.Any(x => x.Id == ideaEntity.Id).ShouldBeFalse();
            userContextMock.VerifyGet(x => x.Id, Times.Once);
            userContextMock.Verify(x => x.IsInRole(RoleName.Admin), Times.Once);

            #endregion
        }

        [Fact(DisplayName = "Ideas.DeleteIdeaHandler_Should_ThrowException_When_IdeaIsBeingDeletedNotByAuthorNorAdmin")]
        public void DeleteIdeaHandler_Should_ThrowException_When_IdeaIsBeingDeletedNotByAuthorNorAdmin()
        {
            #region Arrange

            //insert category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            //insert subcategories bound to the category
            var subcategoryEntities = new List<IdeaSubcategory>()
            {
                new IdeaSubcategory() { Id = 1, IdeaCategoryId = categoryEntity.Id, Name = "subcategory1" },
                new IdeaSubcategory() { Id = 2, IdeaCategoryId = categoryEntity.Id, Name = "subcategory2" }
            };
            _uow.IdeaSubcategories.AddRange(subcategoryEntities);
            _uow.SaveChanges();

            //insert idea
            var ideaEntity = new Idea()
            {
                Id = 1,
                IdeaCategoryId = categoryEntity.Id,
                Title = "Sample title",
                AspNetUserId = 1,
                User = new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                CreatedDate = new DateTime(2017, 10, 16, 8, 34, 40),
                Description = "Sample description"
            };
            _uow.Ideas.Add(ideaEntity);
            _uow.SaveChanges();

            //assign subcategories to idea
            var assignedSubcategories = new List<AssignedIdeaSubcategory>();
            foreach (var subcategoryEntity in subcategoryEntities)
                assignedSubcategories.Add(new AssignedIdeaSubcategory() { IdeaId = ideaEntity.Id, IdeaSubcategoryId = subcategoryEntity.Id });
            _uow.AssignedIdeaSubcategories.AddRange(assignedSubcategories);
            _uow.SaveChanges();

            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.SetupGet(x => x.Id)
                .Returns(123);
            userContextMock.Setup(x => x.IsInRole(RoleName.Admin))
                .Returns(false);

            var command = new DeleteIdea() { IdeaId = ideaEntity.Id.ToString() };
            var handler = new DeleteIdeaHandler(_uow, userContextMock.Object);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(command));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<AccessDeniedException>();
            userContextMock.VerifyGet(x => x.Id, Times.AtMostOnce);
            userContextMock.Verify(x => x.IsInRole(RoleName.Admin), Times.AtMostOnce);

            #endregion
        }

        [Fact(DisplayName = "Ideas.DeleteIdeaHandler_Should_ThrowException_When_IdeaDoesNotExist")]
        public void DeleteIdeaHandler_Should_ThrowException_When_IdeaDoesNotExist()
        {
            #region Arrange
            
            Mock<IUserContext> userContextMock = new Mock<IUserContext>();

            var command = new DeleteIdea() { IdeaId = "123" };
            var handler = new DeleteIdeaHandler(_uow, userContextMock.Object);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(command));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<IdeaNotFoundException>();

            #endregion
        }
    }
}
