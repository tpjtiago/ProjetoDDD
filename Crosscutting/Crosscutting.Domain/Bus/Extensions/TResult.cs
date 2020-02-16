using System.Collections.Generic;

namespace Crosscutting.Domain.Bus.Extensions
{
    public class TResult<T>
    {
        public TResult() { }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }
    }
}