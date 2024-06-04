using AutoMapper;
using FilmoSearchPortal.Application.DTO.Actor;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public sealed class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, ActorDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateActorCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ActorDto> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ActorEntity>(request.ActorForCreating);

            _repositoryManager.ActorRepository.CreateActor(entity);

            await _repositoryManager.SaveAsync();

            var actorToReturn = _mapper.Map<ActorDto>(entity);

            return actorToReturn;
        }
    }
}
