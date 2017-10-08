using AutoMapper;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.Domain.Categories.Commands;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Categories.Models;
using MediatR;
using System;
using System.Linq;

namespace Ideas.Domain.Categories.CommandHandlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, Category>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Category Handle(CreateCategory message)
        {
            //check if category with this name already exists
            if (_uow.IdeaCategories.Any(x => x.Name == message.Name))
                throw new CategoryAlreadyExistsException();

            var categoryEntity = new IdeaCategory()
            {
                CreatedDate = DateTime.UtcNow,
                Name = message.Name
            };

            _uow.IdeaCategories.Add(categoryEntity);
            _uow.SaveChanges();

            return _mapper.Map<Category>(categoryEntity);
        }
    }
}
