using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class CreateDoors : StepOperation<Car>
    {
        public override Car Execute(Car data)
        {
            data.NumberOfDoors = 5;

            return data;
        }
    }
}
