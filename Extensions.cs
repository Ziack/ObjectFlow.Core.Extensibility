using ObjectFlow.Core.Extensibility.Framework;
using Rainbow.ObjectFlow.Constraints;
using Rainbow.ObjectFlow.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rainbow.ObjectFlow.Framework
{
    public static class Extensions
    {
        public static Workflow<T> FromDefinition<T>(this IDefine<T> workflow, WorkflowDefinition<T> workflowDefinition) where T : class
        {
            var _workflow = workflow as Workflow<T>;

            if (null == workflow || null == _workflow)
                throw new InvalidCastException(string.Format("Couldn't cast to workflow<{0}>", typeof(T).ToString()));

            var wdWithoutDependsOn = workflowDefinition.OrderBy(t => t.Priority).Where(t => !t.DependsOn.Any());
            var wdQueued = new HashSet<string>();

            _workflow.AddTheseOperations(wdWithoutDependsOn, wdQueued, concurrent: false);

            return _workflow.FromDefinitionInternal(workflowDefinition, wdQueued);
        }

        public static ResumingDefineBuilder<T> Resume<T>(this IDefine<T> workflow, T state) where T : class
        {
            return new ResumingDefineBuilder<T>(state, workflow);
        }

        internal static Workflow<T> FromDefinitionInternal<T>(this Workflow<T> workflow, WorkflowDefinition<T> workflowDefinition, HashSet<string> wdQueued) where T : class
        {
            while (workflowDefinition.Any())
            {
                workflowDefinition.RemoveAll(t => wdQueued.Any(r => t.Name.Equals(r, StringComparison.InvariantCultureIgnoreCase)));

                var candidates = workflowDefinition.Where(t => t.DependsOn.All(r => wdQueued.Contains(r))).ToList();

                workflow.AddTheseOperations(candidates, wdQueued, concurrent: false);
            }

            return workflow;
        }


        private static Workflow<T> AddTheseOperations<T>(this Workflow<T> workflow, IEnumerable<Stage<T>> candidates, HashSet<string> wdQueued, bool concurrent = false) where T : class
        {
            if (candidates.Any())
            {
                var candidate = candidates.First();

                workflow.AddThisOperation(candidate, concurrent: false);

                wdQueued.Add(candidate.Name);

                Array.ForEach(candidates.Skip(1).Take(candidates.Count()).ToArray(), t =>
                {
                    workflow.AddThisOperation(t, concurrent);
                    wdQueued.Add(t.Name);
                });

                workflow.Then();
            }

            return workflow;
        }

        private static Workflow<T> AddThisOperation<T>(this Workflow<T> workflow, Stage<T> candidate, bool concurrent = false) where T : class
        {
            var scope = candidate.Scope ?? new LocalScope<T>();
            var condition = new Condition(() => candidate.Constraint == null || candidate.Constraint.Check(candidate.Operation));
            var operation = new ScopedOperation<T>(candidate.Operation, scope);

            if (concurrent)
                workflow.And.Do(operation, condition);
            else
                workflow.Do(operation, condition);

            return workflow;
        }
    }
}
