using MediatR;

namespace MyProject.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommand : IRequest<int>
    {
        public string Nombre { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

    }
}
