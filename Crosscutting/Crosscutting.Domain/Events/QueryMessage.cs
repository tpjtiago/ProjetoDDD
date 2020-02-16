using MediatR;

namespace Crosscutting.Domain.Events
{
    public class QueryMessage<TResponse> : IRequest<TResponse>, IRequestBase
    {
        public string MessageType { get; set; }

        protected QueryMessage()
        {
            MessageType = GetType().Name;
        }
    }
}