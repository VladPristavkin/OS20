using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public sealed class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteActorCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _repositoryManager.ActorRepository
                .GetActorByIdAsync(request.ActorId, request.TrackChanges, cancellationToken) ??
                throw new ActorNotFoundException(request.ActorId);

            _repositoryManager.ActorRepository.DeleteActor(actor);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
