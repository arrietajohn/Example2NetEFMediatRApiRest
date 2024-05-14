using AutoMapper;
using MyProject.Application.Contracts.Infrastructure;
using MyProject.Application.Contracts.Persistence;
using MyProject.Application.Models;
using MyProject.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MyProject.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailservice, ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
            var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {newStreamer.Id} fue creado existosamente");

            await SendEmail(newStreamer);

            return newStreamer.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "elcostenioarrieta@gmail.com",
                Body = "La compania de streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailservice.SendEmail(email);
            }
            catch (Exception ex) {
                _logger.LogError($"Errores enviando el email de {streamer.Id}");
            }

        }

    }
}
