using Crosscutting.Domain.Model;

namespace ProjetoDDD.Domain.Models
{
    /// <summary>
    /// Classe modelo que ira representar a collection do MongoDb.
    /// Deve ser herdada a entidade Entity da Crosscutting, pois a mesma habilitara o uso do
    /// repositório genérico
    /// </summary>
    public class DemoModel : Entity
    {
        public string Description { get; set; }
    }
}