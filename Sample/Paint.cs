using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class Paint : StepOperation<Car>
    {
        public override Car Execute(Car data)
        {
            data.Color = "Black";

            return data;
        }
    }
}
