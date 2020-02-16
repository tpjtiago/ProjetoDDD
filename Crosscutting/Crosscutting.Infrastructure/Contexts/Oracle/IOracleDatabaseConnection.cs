using System.Data.OracleClient;

namespace Crosscutting.Infrastructure.Contexts
{
    public interface IOracleDatabaseConnection
    {
        OracleConnection GetConnection();
    }
}