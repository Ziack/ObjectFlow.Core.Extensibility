using ObjectFlow.Core.Extensibility.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Interfaces
{
    public interface IStepOperationConstraint<T>
    {
        bool Check(StepOperation<T> operation);
    }
}
