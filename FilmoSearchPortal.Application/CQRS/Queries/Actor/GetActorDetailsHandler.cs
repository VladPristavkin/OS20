using AutoMapper;
using FilmoSearchPortal.Application.DTO.Actor;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Actor
{
    internal sealed class GetActorDetailsHandler : IRequestHandler<GetActorDetailsQuery, ActorDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetActorDetailsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ActorDto> Handle(GetActorDetailsQuery request, CancellationToken cancellationToken)
        {
            var actor = await _repositoryManager.ActorRepository
                .GetActorByIdAsync(request.ActorId, request.TrackChanges, cancellationToken) ??
                throw new ActorNotFoundException(request.ActorId);

            var actorDto = _mapper.Map<ActorDto>(actor);

            return actorDto;
        }
    }
}
