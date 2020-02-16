using ProjetoDDD.Domain.Commands;

namespace ProjetoDDD.Domain.Validations
{
    public class RemoveDemoValidation : DemoValidation<RemoveDemoCommand>
    {
        public RemoveDemoValidation()
        {
            ValidateId();
        }
    }
}