using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public interface IStateResumer<T> where T : class
    {
        WorkflowDefinition<T> ResumeFromState(T state, WorkflowDefinition<T> workflow);
    }

    public class FromStartResumer<T> : IStateResumer<T> where T : class
    {
        public WorkflowDefinition<T> ResumeFromState(T state, WorkflowDefinition<T> workflow)
        {
            return workflow;
        }
    }

    public class DelegatingResumer<T> : IStateResumer<T> where T : class
    {
        private Func<T, WorkflowDefinition<T>, WorkflowDefinition<T>> _resumeDelegate;

        public DelegatingResumer(Func<T, WorkflowDefinition<T>, WorkflowDefinition<T>> resumeDelegate)
        {
            _resumeDelegate = resumeDelegate;
        }

        public WorkflowDefinition<T> ResumeFromState(T state, WorkflowDefinition<T> workflow)
        {
            return _resumeDelegate(state, workflow);
        }
    }
}
