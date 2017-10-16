using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Common.Context;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Ideas.CommandHandlers;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using Ideas.Domain.Ideas.QueryHandlers;
using Ideas.UnitTests.TestDoubles;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.UnitTests.DomainTests.Ideas
{
    public class CreateIdeaTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateIdeaTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Ideas.CreateIdeaHandler_Should_CreateIdeaAndAllNewSubcategories_When_AllDataIsCorrect")]
        public async Task CreateIdeaHandler_Should_CreateIdeaAndAllNewSubcategories_When_AllDataIsCorrect()
        {
            #region Arrange

            //insert user
            var userEntity = new Entities.Identity.User()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            _uow.Users.Add(userEntity);
            _uow.SaveChanges();

            //insert category
            var categoryEntity = new IdeaCategory() { Id = 1, Name = "category" };
            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            //this will be the actual result of the command,
            //but we need to specify it here because we mock IMediatr
            //to handle returning idea details (as a chained query)
            //and need to execute GetIdeaDetailsHandler manually
            IdeaDetails createdIdea = null;

            //mock user context
            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.SetupGet(x => x.Id)
                .Returns(1);
            userContextMock.Setup(x => x.IsInRole(RoleName.Admin))
                .Returns(false);

            //mock mediator
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetIdeaDetails>(), It.IsAny<CancellationToken>()))
                .Callback((IRequest<IdeaDetails> ideaDetails, CancellationToken token) => 
                {
                    var getIdeaDetailsHandler = new GetIdeaDetailsHandler(_uow, _mapper);
                    createdIdea = getIdeaDetailsHandler.Handle((GetIdeaDetails)ideaDetails);
                })
                .ReturnsAsync(() => createdIdea);

            var command = new CreateIdea()
            {
                CategoryId = categoryEntity.Id.ToString(),
                Description = "Sample description",
                Title = "Sample title",
                Subcategories = new List<string>() { ".net", "angular", "fullstack" }
            };
            var handler = new CreateIdeaHandler(_uow, userContextMock.Object, mediatorMock.Object);

            #endregion

            #region Act

            IdeaDetails result = await handler.Handle(command);

            #endregion

            #region Assert

            //check added idea entity
            _uow.Ideas.ShouldHaveSingleItem();
            var ideaEntity = _uow.Ideas.First();
            ideaEntity.AspNetUserId.ShouldBe(userEntity.Id);
            ideaEntity.Description.ShouldBe(command.Description);
            ideaEntity.IdeaCategoryId.ShouldBe(int.Parse(command.CategoryId));
            ideaEntity.Title.ShouldBe(command.Title);

            //check if subcategories were added
            var subcategoriesEntities = _uow.IdeaSubcategories.ToList();
            foreach(string subcategoryName in command.Subcategories)
            {
                var subcategoryEntity = subcategoriesEntities.FirstOrDefault(x => x.Name == subcategoryName && x.IdeaCategoryId == categoryEntity.Id);
                subcategoryEntity.ShouldNotBeNull();
            }

            //check if subcategories were assigned to the idea
            var assignedSubcategories = _uow.AssignedIdeaSubcategories.ToList();
            foreach(var subcategoryEntity in subcategoriesEntities)
            {
                var assignedSubcategory = assignedSubcategories.FirstOrDefault(x => x.IdeaId == ideaEntity.Id && x.IdeaSubcategoryId == subcategoryEntity.Id);
                assignedSubcategory.ShouldNotBeNull();
            }

            result.ShouldBeSameAs(createdIdea);

            #endregion
        }

    }
}
