using AutoMapper;
using FilmoSearchPortal.Application.DTO.Director;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;
using DirectorEntity = FilmoSearchPortal.Domain.Models.Director;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public sealed class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, DirectorDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateDirectorCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<DirectorDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DirectorEntity>(request.DirectorForCreating);

            _repositoryManager.DirectorRepository.CreateDirector(entity);

            await _repositoryManager.SaveAsync();

            var directorToReturn = _mapper.Map<DirectorDto>(entity);

            return directorToReturn;
        }
    }
}
