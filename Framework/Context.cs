using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public class Context : ConcurrentDictionary<String, Object>, IDictionary<string, object>
    {
        public Guid IdAccion { get; private set; }

        public Context() : this(Guid.NewGuid()) { }

        public Context(Guid id)
        {
            IdAccion = id;
        }

        public Context(IDictionary<string, object> source)
            : this(Guid.NewGuid(), source)
        { }

        public Context(Guid id, IDictionary<string, object> source)
            : base(source)
        {
            IdAccion = id;
        }

        public TValue Get<TValue>(string key)
        {
            try
            {
                if (this[key] is JObject)
                {
                    return ((JObject)this[key]).ToObject<TValue>();
                }
                else
                    return (TValue)this[key];

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Key not found: {key}", ex);
            }
        }

        public TValue TryGet<TValue>(string key)
        {
            object value;
            TryGetValue(key, out value);

            if (value == null)
                return default(TValue);

            return (TValue)value;
        }

        /// <summary>
        /// Trata un objeto cuya key coincida con el nombre
        /// no calificado de su clase.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public TValue Get<TValue>()
        {
            return Get<TValue>(typeof(TValue).Name);
        }

        public TValue TryGet<TValue>()
        {
            return TryGet<TValue>(typeof(TValue).Name);
        }

        public void Set<TValue>(TValue value)
        {
            Set(typeof(TValue).Name, value);
        }

        public void Set<TValue>(string name, TValue value)
        {
            this[name] = value;
        }


        /// <summary>
        /// Un alias para ContainsKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Tiene(string key)
        {
            return ContainsKey(key);
        }

        public bool Tiene<TValue>()
        {
            return ContainsKey(typeof(TValue).Name);
        }
    }
}
