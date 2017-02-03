using ObjectFlow.Core.Extensibility.Interfaces;
using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ObjectFlow.Core.Extensibility.Framework
{
    [DataContract]
    public class Stage<T> : ICloneable
    {
        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "displayName")]
        public String DisplayName { get; set; }

        [DataMember(Name = "priority")]
        public Int32 Priority { get; set; }

        [DataMember(Name = "handler")]
        public StepOperation<T> Operation { get; set; }

        [DataMember(Name = "dependsOn")]
        public IList<String> DependsOn { get; set; }

        [DataMember(Name = "scope")]
        public IOperationScope<T> Scope { get; set; }

        [DataMember(Name = "constraints")]
        public IStepOperationConstraint<T> Constraint { get; set; }

        [DataMember(Name = "params")]
        public IDictionary<String, String> Params { get; set; }

        public Stage()
        {
            DependsOn = new List<String>();
            Params = new Dictionary<String, String>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
