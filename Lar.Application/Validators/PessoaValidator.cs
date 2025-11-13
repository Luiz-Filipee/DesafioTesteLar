using FluentValidation;
using Lar.Application.DTOs;

namespace Lar.Application.Validators;

public class CreatePessoaDtoValidator : AbstractValidator<CreatePessoaDto>
{
    public CreatePessoaDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}

public class UpdatePessoaDtoValidator : AbstractValidator<UpdatePessoaDto>
{
    public UpdatePessoaDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
