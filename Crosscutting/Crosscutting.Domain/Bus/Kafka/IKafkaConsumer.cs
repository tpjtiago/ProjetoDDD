using Crosscutting.Domain.Bus.Extensions;
using System.Threading.Tasks;

namespace Crosscutting.Domain.Bus
{
    public interface IKafkaConsumer
    {
        Task<TResult<T>> Consume<T>(string topic);
    }
}