using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    public class CreateSeats : StepOperation<Car>
    {
        public override Car Execute(Car data)
        {
            data.NumberOfSeats = 4;

            return data;
        }
    }
}
