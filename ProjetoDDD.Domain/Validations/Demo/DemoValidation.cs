using FluentValidation;
using ProjetoDDD.Domain.Commands;

namespace ProjetoDDD.Domain.Validations
{
    public class DemoValidation<T> : AbstractValidator<T> where T : DemoCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("O código é obrigatório");
        }

        protected void Validate()
        {
            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("A descrição é obrigatória");
        }
    }
}