using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class CreateGearSystem : StepOperation<Car>
    {
        public override Car Execute(Car data)
        {
            data.GearSystem = "Mechanical";

            return data;
        }
    }
}
