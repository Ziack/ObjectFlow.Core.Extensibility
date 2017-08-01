using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class CreateWheels : StepOperation<Car>
    {
        public override Car Execute(Car data)
        {
            data.NumberOfWheels = 4;

            return data;
        }
    }
}
