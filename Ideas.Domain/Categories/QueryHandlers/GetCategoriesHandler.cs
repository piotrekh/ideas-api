using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Ideas.Domain.Categories.QueryHandlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategories, ItemsResult<Category>>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public GetCategoriesHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ItemsResult<Category> Handle(GetCategories message)
        {
            var categoriesEntities = _uow.IdeaCategories.ToList();

            var categories = new ItemsResult<Category>();
            categories.Items = _mapper.Map<List<Category>>(categoriesEntities);

            return categories;
        }
    }
}
