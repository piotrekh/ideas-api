using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Ideas.Domain.Categories.CommandHandlers
{
    public class GetSubcategoriesHandler : IRequestHandler<GetSubcategories, ItemsResult<Subcategory>>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public GetSubcategoriesHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ItemsResult<Subcategory> Handle(GetSubcategories message)
        {
            int categoryId = int.Parse(message.CategoryId);

            //check if category with this id exists
            if (!_uow.IdeaCategories.Any(x => x.Id == categoryId))
                throw new InvalidCategoryIdException();

            var subcategories = _uow.IdeaSubcategories.Where(x => x.IdeaCategoryId == categoryId)
                .ToList();

            var result = new ItemsResult<Subcategory>();
            result.Items = _mapper.Map<List<Subcategory>>(subcategories);

            return result;
        }
    }
}
