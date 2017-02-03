using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public class StepOperationCompleteEventArgs<TInput> : ActionEventArgs
        where TInput : class
    {
        public TInput Message { get; private set; }
        public Context Context { get; private set; }
        public StepOperation<TInput> Handler { get; private set; }

        public StepOperationCompleteEventArgs(TInput message, StepOperation<TInput> handler, Context context, Exception exception = null)
            : base(exception)
        {
            Message = message;
            Handler = handler;
            Context = context;
        }
    }
}
