using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Exceptions
{
    [Serializable]
    public class InicializacionDeAccionException : Exception
    {

        public InicializacionDeAccionException() { }
        public InicializacionDeAccionException(string message) : base(message) { }
        public InicializacionDeAccionException(string message, Exception inner) : base(message, inner) { }
        protected InicializacionDeAccionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
