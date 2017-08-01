using Rainbow.ObjectFlow.Framework;
using System;
using System.IO;
using System.Reflection;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class CarAssembly
    {
        public Car Build()
        {
            var rawAssemblyDefinition = LoadAssemblyDefinition();
            var assemblyDefinition = rawAssemblyDefinition.ToWorkflowDefinition<Car>();
            var car = Workflow<Car>.Definition().FromDefinition(assemblyDefinition).Start(new Car { });
            return car;
        }

        private static String LoadAssemblyDefinition()
        {
            var resourceName = "ObjectFlow.Core.Extensibility.Sample.assemblyDefinition.json";

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
