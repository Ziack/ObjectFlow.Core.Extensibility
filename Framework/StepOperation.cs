using System;
using Rainbow.ObjectFlow.Interfaces;
using Rainbow.ObjectFlow.Framework;
using System.Collections.Generic;

namespace ObjectFlow.Core.Extensibility.Framework
{
    /// <summary>
    /// Implements common functionality for Operation objects.  
    /// All operations must inherit from StepOperation.
    /// </summary>
    /// <typeparam name="T">Type of object this operation will work on.</typeparam>
    public abstract class StepOperation<T> : BasicOperation<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public delegate void EventTrackedHandler(IOperation<T> operation, EventType eventType, String message);

        /// <summary>
        /// 
        /// </summary>
        public event EventTrackedHandler EventTracked;

        /// <summary>
        /// Default constructor
        /// </summary>
        public StepOperation() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public StepOperation(Context context)
            : this()
        {
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public StepOperation(Context context, IDictionary<String, String> @params = null)
            : this()
        {
            Context = context;
            Params = @params;
        }

        /// <summary>
        /// 
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<String, String> Params { get; private set; }

        public virtual String QualifyName { get { return this.GetType().AssemblyQualifiedName; } }

        public virtual void TrackEvent(String message, EventType eventType)
        {
            if (EventTracked != null)
                EventTracked(operation: this, eventType: eventType, message: message);
        }

    }
}