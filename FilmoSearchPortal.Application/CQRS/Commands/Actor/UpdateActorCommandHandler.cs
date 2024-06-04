using AutoMapper;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public sealed class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateActorCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _repositoryManager.ActorRepository
                .GetActorByIdAsync(request.ActorId, request.TrackChanges, cancellationToken) ??
                throw new ActorNotFoundException(request.ActorId);

            _mapper.Map(request.ActorForUpdate, actor);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
