using ProjetoDDD.Domain.Commands;

namespace ProjetoDDD.Domain.Validations
{
    public class AddDemoValidation : DemoValidation<AddDemoCommand>
    {
        public AddDemoValidation()
        {
            Validate();
        }
    }
}