using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Ideas.Domain.Ideas.QueryHandlers
{
    public class GetIdeasFromCategoryHandler : IRequestHandler<GetIdeasFromCategory, ItemsResult<Idea>>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public GetIdeasFromCategoryHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ItemsResult<Idea> Handle(GetIdeasFromCategory message)
        {
            int categoryId = int.Parse(message.CategoryId);

            //check if category with this id exists
            if (!_uow.IdeaCategories.Any(x => x.Id == categoryId))
                throw new InvalidCategoryIdException();

            var ideasEntities = _uow.Ideas.Where(x => x.IdeaCategoryId == categoryId)
                .ToList();

            var ideas = _mapper.Map<List<Idea>>(ideasEntities);

            return new ItemsResult<Idea>() { Items = ideas };
        }
    }
}
