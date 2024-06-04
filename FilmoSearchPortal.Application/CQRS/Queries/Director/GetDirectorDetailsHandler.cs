using AutoMapper;
using FilmoSearchPortal.Application.DTO.Director;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Director
{
    internal sealed class GetDirectorDetailsHandler : IRequestHandler<GetDirectorDetailsQuery, DirectorDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetDirectorDetailsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<DirectorDto> Handle(GetDirectorDetailsQuery request, CancellationToken cancellationToken)
        {
            var director = await _repositoryManager.DirectorRepository
                .GetDirectorByIdAsync(request.DirectorId, request.TrackChanges, cancellationToken) ??
                throw new DirectorNotFoundException(request.DirectorId);

            var directorDto = _mapper.Map<DirectorDto>(director);

            return directorDto;
        }
    }
}
