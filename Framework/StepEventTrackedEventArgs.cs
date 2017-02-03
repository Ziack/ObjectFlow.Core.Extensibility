using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public class StepEventTrackedEventArgs<TInput> : ActionEventArgs
        where TInput : class
    {
        public String Message { get; private set; }

        public Context Context { get; private set; }

        public EventType EventType { get; private set; }

        public StepOperation<TInput> Handler { get; private set; }

        public StepEventTrackedEventArgs(EventType eventType, String message, StepOperation<TInput> handler, Context context)
            : base(ex: null)
        {
            this.Message = message;
            this.Handler = handler;
            this.EventType = eventType;
            this.Context = context;
        }
    }
}
