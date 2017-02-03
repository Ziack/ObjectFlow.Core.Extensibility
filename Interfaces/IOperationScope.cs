using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Interfaces
{
    public interface IOperationScope<T>
    {
        string Name { get; }

        T Execute(IOperation<T> operation, T data);

    }
}
