using Ideas.DataAccess;
using Ideas.Domain.Common.Context;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Exceptions;
using Ideas.Domain.Users.Exceptions;
using MediatR;
using System.Linq;

namespace Ideas.Domain.Ideas.CommandHandlers
{
    public class DeleteIdeaHandler : IRequestHandler<DeleteIdea>
    {
        private IUnitOfWork _uow;
        private IUserContext _userContext;

        public DeleteIdeaHandler(IUnitOfWork uow,
            IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public void Handle(DeleteIdea message)
        {
            //check if idea exists
            int ideaId = int.Parse(message.IdeaId);
            var ideaEntity = _uow.Ideas.SingleOrDefault(x => x.Id == ideaId);
            if (ideaEntity == null)
                throw new IdeaNotFoundException();

            //assure that only the author or an administrator can delete an idea
            if(_userContext.Id.GetValueOrDefault() != ideaEntity.AspNetUserId
                && !_userContext.IsInRole(RoleName.Admin))
            {
                throw new AccessDeniedException();
            }

            using (var transaction = _uow.BeginTransaction())
            {
                try
                {
                    //delete assigned subcategories
                    _uow.BatchDelete(_uow.AssignedIdeaSubcategories.Where(x => x.IdeaId == ideaId));

                    //delete ideas
                    _uow.BatchDelete(_uow.Ideas.Where(x => x.Id == ideaId));

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
