using AutoMapper;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public sealed class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _repositoryManager.DirectorRepository
                .GetDirectorByIdAsync(request.DirectorId, request.TrackChanges, cancellationToken) ??
                throw new DirectorNotFoundException(request.DirectorId);

            _mapper.Map(request.DirectorForUpdateDto, director);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
