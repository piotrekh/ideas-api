using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ideas.Domain.Ideas.QueryHandlers
{
    public class GetIdeasFromSubcategoryHandler : IRequestHandler<GetIdeasFromSubcategory, ItemsResult<Idea>>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public GetIdeasFromSubcategoryHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ItemsResult<Idea> Handle(GetIdeasFromSubcategory message)
        {
            int categoryId = int.Parse(message.CategoryId);
            int subcategoryId = int.Parse(message.SubcategoryId);

            //check if category with this id exitsts
            if (!_uow.IdeaCategories.Any(x => x.Id == categoryId))
                throw new InvalidCategoryIdException();

            //check if subcategory with this id exists
            var subcategory = _uow.IdeaSubcategories.FirstOrDefault(x => x.Id == subcategoryId);
            if (subcategory == null)
                throw new SubcategoryNotFoundException();
            //check if subcategory is assigned to the category
            if (subcategory.IdeaCategoryId != categoryId)
                throw new InvalidSubcategoryIdException();

            var ideasEntities = _uow.AssignedIdeaSubcategories.Include(x => x.Idea)
                .Where(x => x.IdeaSubcategoryId == subcategoryId)
                .Select(x => x.Idea)
                .ToList();

            var ideas = _mapper.Map<List<Idea>>(ideasEntities);

            return new ItemsResult<Idea>() { Items = ideas };
        }
    }
}
