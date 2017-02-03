using ObjectFlow.Core.Extensibility.Framework;
using Rainbow.ObjectFlow.Framework;
using Rainbow.ObjectFlow.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public class ResumingDefineBuilder<T> where T : class
    {
        private T _state;
        private IDefine<T> _workflow;
        private IStateResumer<T> _resumer;

        internal ResumingDefineBuilder(T state, IDefine<T> workflow)
        {
            _state = state;
            _workflow = workflow;
            _resumer = new FromStartResumer<T>();
        }


        public ResumingDefineBuilder<T> Using<TResumer>()
            where TResumer : IStateResumer<T>, new()
        {
            _resumer = Activator.CreateInstance<TResumer>();

            return this;
        }
     

        ResumingDefineBuilder<T> Using(Func<T, WorkflowDefinition<T>, WorkflowDefinition<T>> resumeDelegate)
        {
            _resumer = new DelegatingResumer<T>(resumeDelegate);

            return this;
        }

        public Workflow<T> FromDefinition(WorkflowDefinition<T> workflowDefinition)
        {
            var notRanStages = _resumer.ResumeFromState(_state, workflowDefinition);

            var workflow = _workflow as Workflow<T>;

            if (null == _workflow || null == workflow)
                throw new InvalidCastException(string.Format("Couldn't cast to workflow<{0}>", typeof(T).ToString()));

            var alreadyRanStages = workflowDefinition
                    .Where(original => !notRanStages.Any(d => original.Name.Equals(d.Name, StringComparison.InvariantCultureIgnoreCase)))
                    .Select(d => d.Name);

            var wdQueued = new HashSet<string>(alreadyRanStages);

            return workflow.FromDefinitionInternal(notRanStages, wdQueued);
        }

    }
}
