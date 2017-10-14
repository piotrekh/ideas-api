using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Ideas.Exceptions;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ideas.Domain.Ideas.QueryHandlers
{
    public class GetIdeaDetailsHandler : IRequestHandler<GetIdeaDetails, IdeaDetails>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetIdeaDetailsHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IdeaDetails Handle(GetIdeaDetails message)
        {
            int ideaId = int.Parse(message.IdeaId);

            var ideaEntity = _uow.Ideas.Include(x => x.User)
                .Include(x => x.Subcategories).ThenInclude(x => x.Subcategory)
                .Include(x => x.Category)
                .Where(x => x.Id == ideaId)
                .SingleOrDefault();

            if (ideaEntity == null)
                throw new IdeaNotFoundException();

            var idea = _mapper.Map<IdeaDetails>(ideaEntity);
            return idea;
        }
    }
}
