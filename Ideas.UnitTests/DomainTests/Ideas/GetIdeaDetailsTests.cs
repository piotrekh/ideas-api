using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Ideas.Exceptions;
using Ideas.Domain.Ideas.Queries;
using Ideas.Domain.Ideas.QueryHandlers;
using Ideas.UnitTests.TestDoubles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ideas.UnitTests.DomainTests.Ideas
{
    public class GetIdeaDetailsTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetIdeaDetailsTests()
        {
            _uow = new InMemoryUnitOfWork();
            _mapper = MapperFactory.CreateMapper();
        }

        [Fact(DisplayName = "Ideas.GetIdeaDetailsHandler_Should_ReturnIdeaDetails_When_IdeaExists")]
        public void GetIdeaDetailsHandler_Should_ReturnIdeaDetails_When_IdeaExists()
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
                User = new DataAccess.Entities.Identity.User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                CreatedDate = new DateTime(2017, 10, 14, 20,36,20),
                Description = "Sample description"                
            };
            _uow.Ideas.Add(ideaEntity);
            _uow.SaveChanges();

            //assign subcategories to idea
            var assignedSubcategories = new List<AssignedIdeaSubcategory>();
            foreach(var subcategoryEntity in subcategoryEntities)
                assignedSubcategories.Add(new AssignedIdeaSubcategory() { IdeaId = ideaEntity.Id, IdeaSubcategoryId = subcategoryEntity.Id });
            _uow.AssignedIdeaSubcategories.AddRange(assignedSubcategories);
            _uow.SaveChanges();

            var query = new GetIdeaDetails() { IdeaId = ideaEntity.Id.ToString() };
            var handler = new GetIdeaDetailsHandler(_uow, _mapper);

            #endregion

            #region Act

            var idea = handler.Handle(query);

            #endregion

            #region Assert

            idea.ShouldNotBeNull();
            idea.Author.ShouldNotBeNull();
            idea.Author.FirstName.ShouldBe(ideaEntity.User.FirstName);
            idea.Author.LastName.ShouldBe(ideaEntity.User.LastName);
            idea.Category.ShouldNotBeNull();
            idea.Category.Name.ShouldBe(categoryEntity.Name);
            idea.Category.Id.ShouldBe(categoryEntity.Id.ToString());
            idea.CreatedDate.ShouldBe(ideaEntity.CreatedDate);
            idea.Description.ShouldBe(ideaEntity.Description);
            idea.Id.ShouldBe(ideaEntity.Id.ToString());
            idea.Subcategories.ShouldNotBeEmpty();
            foreach (var subcategoryEntity in subcategoryEntities)
                idea.Subcategories.FirstOrDefault(x => x.Id == subcategoryEntity.Id.ToString()).ShouldNotBeNull();
            idea.Title.ShouldBe(ideaEntity.Title);

            #endregion
        }

        [Fact(DisplayName = "Ideas.GetIdeaDetailsHandler_Should_ThrowException_When_IdeaDoesNotExists")]
        public void GetIdeaDetailsHandler_Should_ThrowException_When_IdeaDoesNotExists()
        {
            #region Arrange            
            
            var query = new GetIdeaDetails() { IdeaId = "123" };
            var handler = new GetIdeaDetailsHandler(_uow, _mapper);

            #endregion

            #region Act

            Exception ex = Record.Exception(() => handler.Handle(query));

            #endregion

            #region Assert

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<IdeaNotFoundException>();

            #endregion
        }
    }
}

