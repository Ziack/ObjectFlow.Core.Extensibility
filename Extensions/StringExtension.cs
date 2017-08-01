using Newtonsoft.Json;
using ObjectFlow.Core.Extensibility.Framework;
using ObjectFlow.Core.Extensibility.Interfaces;
using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectFlow.Core.Extensibility
{
    public static class StringExtension
    {
        public static WorkflowDefinition<T> ToWorkflowDefinition<T>(this String definition, Context context = null)
        {
            return JsonConvert.DeserializeObject<WorkflowDefinition<T>>(
                        definition, new JsonSerializerSettings
                        {
                            Converters = new List<JsonConverter>()
                             {
                                new FromClassNameConverter<IOperation<T>>(context ?? new Context()),
                                new FromClassNameConverter<IStepOperationConstraint<T>>(),
                                new FromClassNameConverter<IOperationScope<T>>(),
                             }
                        }
                    );
        }
    }
}
