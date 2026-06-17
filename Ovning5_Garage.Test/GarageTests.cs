using Ovning5_Garage.Core;
using Ovning5_Garage.Vehicles;

namespace Ovning5_Garage.Test
{
    public class GarageTests
    {
        [Fact]
        public void CreateGarage_ShouldHaveCorrectCapacity()
        {
            //Arrange
            int expectedCapacity = 10;

            //Act
            Garage<Vehicle> myGarage = new Garage<Vehicle>(expectedCapacity);

            //Assert
            Assert.Equal(expectedCapacity, myGarage.Capacity);
        }

        [Fact]
        public void ParkVehicle_ShouldSuccess_WhenGarageHasSpace()
        {
            //Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car testCar = new Car("ABC123", "Vigilante", "Grotti", "Black", "Super");

            //Act
            bool isParked = garage.Park(testCar);

            //Assert
            Assert.True(isParked);
        }

        [Fact]
        public void ParkVehicle_ShouldFail_WhenGarageIsFull()
        {
            //Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car car1 = new Car("ABC123", "Vigilante", "Grotti", "Black", "Super");
            garage.Park(car1);

            Car car2 = new Car("DEF456", "Volitc", "Coil", "White", "Super");

            //Act
            bool isParked = garage.Park(car2);

            //Assert
            Assert.False(isParked);
        }

        [Fact]
        public void ParkVehicle_ShouldFail_WhenRegisterNumberIsDuplicate()
        {
            //Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(3);
            Car carOriginal = new Car("ABC123", "Vigilante", "Grotti", "Black", "Super");
            Car carDuplicate = new Car("ABC123", "Volitc", "Coil", "White", "Super");

            garage.Park(carOriginal);

            //Act
            bool isParked = garage.Park(carDuplicate);

            //Assert
            Assert.False(isParked);
        }

        [Fact]
        public void RemoveVehicle_ShouldReturnFalse_WhenVehicleDoesNotExist()
        {
            //Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car = new Car("ABC123", "Vigilante", "Grotti", "Black", "Super");
            garage.Park(car);

            //Act
            bool isRemoved = garage.Remove("MISSIN");

            //Assert
            Assert.False(isRemoved);
        }
    }
}
