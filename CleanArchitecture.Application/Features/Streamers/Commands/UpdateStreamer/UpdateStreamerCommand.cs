using MediatR;

namespace MyProject.Application.Features.Streamers.Commands.UpdateStreamer;

public  class UpdateStreamerCommand : IRequest 
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}
