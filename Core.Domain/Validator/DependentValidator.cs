using Core.Domain.Entities;
using FluentValidation;

namespace Core.Domain.Validator
{
    public class DependentValidator : AbstractValidator<Dependent>
    {

        public DependentValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(60).WithMessage("O campo Nome do Dependente deve ter no máximo 60 caracteres");

        }


    }
}
