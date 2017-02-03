using ObjectFlow.Core.Extensibility.Interfaces;
using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    internal class LocalScope<T> : IOperationScope<T>
    {
        public string Name
        {
            get { return "Local"; }
        }

        public T Execute(IOperation<T> operation, T data)
        {
            return operation.Execute(data);
        }
    }
}
