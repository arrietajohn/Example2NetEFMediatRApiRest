using FluentValidation;

namespace MyProject.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        RuleFor(p => p.Nombre)
            .NotNull().WithMessage("{Nombre} no permite valores nulos");

        RuleFor(p => p.Url)
            .NotNull().WithMessage("{Ur;} no permite valores nulos");
    }
}
