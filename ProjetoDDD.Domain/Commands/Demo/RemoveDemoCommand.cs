using ProjetoDDD.Domain.Validations;
using System;

namespace ProjetoDDD.Domain.Commands
{
    public class RemoveDemoCommand : DemoCommand
    {
        public RemoveDemoCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveDemoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}