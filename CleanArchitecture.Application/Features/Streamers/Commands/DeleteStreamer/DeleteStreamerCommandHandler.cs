using AutoMapper;
using MyProject.Application.Contracts.Persistence;
using MyProject.Application.Exceptions;
using MyProject.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MyProject.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
{
    private readonly IStreamerRepository _streamerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteStreamerCommandHandler> _logger;

    public DeleteStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger)
    {
        _streamerRepository = streamerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await _streamerRepository.GetByIdAsync(request.Id);
        if (streamerToDelete == null)
        {
            _logger.LogError($"{request.Id} streamer no existe en el sistema");
            throw new NotFoundException(nameof(Streamer), request.Id);      
        }

        await _streamerRepository.DeleteAsync(streamerToDelete);

        _logger.LogInformation($"El {request.Id} streamer fue eliminado con exito");

        return Unit.Value;
    }
}
