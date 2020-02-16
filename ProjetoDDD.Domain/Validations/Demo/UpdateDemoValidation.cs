using ProjetoDDD.Domain.Commands;

namespace ProjetoDDD.Domain.Validations
{
    public class UpdateDemoValidation : DemoValidation<UpdateDemoCommand>
    {
        public UpdateDemoValidation()
        {
            ValidateId();
            Validate();
        }
    }
}