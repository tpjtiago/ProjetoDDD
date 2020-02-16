using ProjetoDDD.Domain.Validations;

namespace ProjetoDDD.Domain.Commands
{
    public class AddDemoCommand : DemoCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddDemoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}