using Ideas.DataAccess;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Context;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.Domain.Ideas.CommandHandlers
{
    public class CreateIdeaHandler : IAsyncRequestHandler<CreateIdea, IdeaDetails>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;
        private readonly IMediator _mediator;

        public CreateIdeaHandler(IUnitOfWork uow,
            IUserContext userContext,
            IMediator mediator)
        {
            _uow = uow;
            _userContext = userContext;
            _mediator = mediator;
        }

        public async Task<IdeaDetails> Handle(CreateIdea message)
        {
            //check if category exists
            int categoryId = int.Parse(message.CategoryId);
            if (!_uow.IdeaCategories.Any(x => x.Id == categoryId))
                throw new InvalidCategoryIdException();

            //save idea
            var ideaEntity = new Entities.Idea()
            {
                AspNetUserId = _userContext.Id.GetValueOrDefault(),
                CreatedDate = DateTime.UtcNow,
                Description = message.Description,
                IdeaCategoryId = categoryId,
                Title = message.Title
            };
            _uow.Ideas.Add(ideaEntity);
            _uow.SaveChanges();

            //assign subcategories - if a subcategory doesn't exist, add it
            foreach(var subcategory in message.Subcategories)
            {
                var subcategoryEntity = AddOrRetrieveSubcategory(subcategory, categoryId, false);

                //skip failures
                if (subcategoryEntity == null)
                    continue;

                var assignedSubcategory = new Entities.AssignedIdeaSubcategory()
                {
                    IdeaId = ideaEntity.Id,
                    IdeaSubcategoryId = subcategoryEntity.Id
                };
                _uow.AssignedIdeaSubcategories.Add(assignedSubcategory);
            }
            _uow.SaveChanges();

            return await _mediator.Send(new GetIdeaDetails() { IdeaId = ideaEntity.Id.ToString() });
        }

        private Entities.IdeaSubcategory AddOrRetrieveSubcategory(string subcategory, int categoryId, bool isRetry)
        {
            //try to retrieve the subcategory
            Entities.IdeaSubcategory subcategoryEntity = _uow.IdeaSubcategories.Where(x => x.IdeaCategoryId == categoryId && x.Name == subcategory)
                    .SingleOrDefault();

            //add the subcategory if it doesn't exist
            if (subcategoryEntity == null)
            {
                using (var transaction = _uow.BeginTransaction())
                {
                    try
                    {
                        subcategoryEntity = new Entities.IdeaSubcategory()
                        {
                            CreatedDate = DateTime.UtcNow,
                            IdeaCategoryId = categoryId,
                            Name = subcategory
                        };
                        _uow.IdeaSubcategories.Add(subcategoryEntity);
                        _uow.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }

            //if insert failed, then it may mean that in the meantime a subcategory
            //with exactly the same name has been inserted - in this case, retry the method
            if (subcategoryEntity != null)
                return subcategoryEntity;
            else if (!isRetry)
                return AddOrRetrieveSubcategory(subcategory, categoryId, true);
            else
                return null;
        }
    }
}
