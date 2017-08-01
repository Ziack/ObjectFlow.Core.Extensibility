using NUnit.Framework;

namespace ObjectFlow.Core.Extensibility.Sample
{
    [TestFixture]
    public class CarAssemblyTests
    {
        [Test]
        public void CarAssembly_RawCar_BuildsCar()
        {
            var createAssembly = new CarAssembly();

            var car = createAssembly.Build();

            Assert.IsNotNull(car);
            Assert.AreEqual("Mechanical", car.GearSystem);
            Assert.AreEqual(4, car.NumberOfSeats);
            Assert.AreEqual(4, car.NumberOfWheels);
            Assert.AreEqual(5, car.NumberOfDoors);
            Assert.AreEqual("Black", car.Color);
        }
    }
}
