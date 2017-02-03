using ObjectFlow.Core.Extensibility.Interfaces;
using Rainbow.ObjectFlow.Framework;
using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public class ScopedOperation<T> : StepOperation<T>
    {
        private StepOperation<T> _originalOperation;
        private IOperationScope<T> _scope;

        public ScopedOperation(StepOperation<T> originalOperation, IOperationScope<T> scope)
            : base(originalOperation.Context)
        {
            _originalOperation = originalOperation;
            _scope = scope;
        }

        public override string QualifyName
        {
            get
            {
                return _originalOperation.QualifyName;
            }
        }

        public override void TrackEvent(string message, EventType eventType)
        {
            _originalOperation.TrackEvent(message: message, eventType: eventType);
        }

        public override T Execute(T data)
        {
            return _scope.Execute(_originalOperation, data);
        }
    }
}
