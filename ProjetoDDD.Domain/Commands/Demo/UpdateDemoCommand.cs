using ProjetoDDD.Domain.Validations;

namespace ProjetoDDD.Domain.Commands
{
    public class UpdateDemoCommand : DemoCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateDemoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}