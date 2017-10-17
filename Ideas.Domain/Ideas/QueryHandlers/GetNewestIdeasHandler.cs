using AutoMapper;
using Ideas.DataAccess;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using Ideas.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Ideas.Domain.Ideas.QueryHandlers
{
    public class GetNewestIdeasHandler : IRequestHandler<GetNewestIdeas, ItemsResult<Idea>>
    {
        private readonly IUnitOfWork _uow;        
        private readonly IdeasSettings _settings;
        private readonly IMapper _mapper;

        public GetNewestIdeasHandler(IUnitOfWork uow,
            IOptions<IdeasSettings> settings,
            IMapper mapper)
        {
            _uow = uow;
            _settings = settings.Value;
            _mapper = mapper;
        }

        public ItemsResult<Idea> Handle(GetNewestIdeas message)
        {
            var ideasEntities = _uow.Ideas.OrderByDescending(x => x.Id)
                .Take(_settings.NewestIdeasNumber)
                .ToList();

            var ideas = new ItemsResult<Idea>();
            ideas.Items = _mapper.Map<List<Idea>>(ideasEntities);

            return ideas;
        }
    }
}
