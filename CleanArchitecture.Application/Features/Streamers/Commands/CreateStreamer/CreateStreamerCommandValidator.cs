using FluentValidation;

namespace MyProject.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

            RuleFor(p => p.Url)
                 .NotEmpty().WithMessage("La {Url} no puede estar en blanco");

        }
    }
}
