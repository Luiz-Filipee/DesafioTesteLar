using FluentValidation;
using Lar.Application.DTOs;

namespace Lar.Application.Validators;

public class CreateUsuarioDtoValidator : AbstractValidator<CreateUsuarioDto>
{
    public CreateUsuarioDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        RuleFor(x => x.PessoaId).NotEmpty();
    }
}

public class UpdateUsuarioDtoValidator : AbstractValidator<UpdateUsuarioDto>
{
    public UpdateUsuarioDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
    }
}
