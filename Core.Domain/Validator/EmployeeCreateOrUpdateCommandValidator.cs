using Core.Domain.Command.Handlers;
using FluentValidation;

namespace Core.Domain.Validator
{
    public class EmployeeCreateOrUpdateCommandValidator : AbstractValidator<EmployeeCreateOrUpdateCommand>
    {
        public EmployeeCreateOrUpdateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo Nome é obrigatório")
                .MaximumLength(60).WithMessage("O campo Nome deve ter no máximo 60 caracteres");

            RuleFor(x => x.Dependent).SetCollectionValidator(new DependentValidator());


            When(x => !string.IsNullOrWhiteSpace(x.Email), () => {
                RuleFor(x => x.Email)
                 .MaximumLength(150).WithMessage("O campo Email deve ter no máximo 150 caracteres")
                 .EmailAddress().WithMessage("O campo Email deve conter um email válido");
            });

        }


    }
}
