using AutoMapper;
using FilmoSearchPortal.Application.DTO.Actor;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Actor
{
    internal sealed class GetActorCollectionHandler : IRequestHandler<GetActorCollectionQuery, IEnumerable<ActorDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetActorCollectionHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorDto>> Handle(GetActorCollectionQuery request, CancellationToken cancellationToken)
        {
            var actors = await _repositoryManager.ActorRepository
                .GetAllActorsAsync(request.TrackChanges, cancellationToken);

            var actorsDto = _mapper.Map<IEnumerable<ActorDto>>(actors);

            return actorsDto;
        }
    }
}
