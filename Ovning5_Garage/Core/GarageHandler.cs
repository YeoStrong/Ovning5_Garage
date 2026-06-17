using Ovning5_Garage.Interfaces;
using Ovning5_Garage.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Core
{
    public class GarageHandler : IGarageHandler
    {
        private Garage<Vehicle> _myGarage;
        private const string FilePath = "garage.json";

        public void CreateGarage(int capacity)
        {
            _myGarage = new Garage<Vehicle>(capacity);
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            if (_myGarage == null) return false;

            return _myGarage.Park(vehicle);
        }

        public bool RemoveVehicle(string regNr)
        {
            if (_myGarage == null) return false;

            return _myGarage.Remove(regNr);
        }

        public string GarageStatus()
        {
            if (_myGarage == null) return "There is no garage.";

            return _myGarage.GetVehicleCounts();
        }

        public IEnumerable<Vehicle> SearchVehicles(Func<Vehicle, bool> predicate)
        {
            if (_myGarage == null) return Enumerable.Empty<Vehicle>();

            return _myGarage.Search(predicate);
        }

        public bool SaveGarageData()
        {
            try
            {
                if (_myGarage == null) return false;

                Vehicle[] vehiclesToSend = _myGarage.ToArray();

                FileService.SaveGarage(FilePath, vehiclesToSend);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadGarageData()
        {
            if (!System.IO.File.Exists(FilePath)) return false;

            try
            {
                Vehicle[] loadedVehicles = FileService.LoadGarage(FilePath);
                int capacity = loadedVehicles.Length + 10;
                _myGarage = new Garage<Vehicle>(capacity);

                foreach (var v in loadedVehicles)
                {
                    if (v != null) _myGarage.Park(v);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SeedData()
        {
            if (_myGarage == null)
            {
                Console.WriteLine("Please create a garage first!");
                return;
            }

            var testVehicles = new List<Vehicle>
    {
        new Car("ABC123", "Model S", "Tesla", "Black", "Sedan"),
        new Car("XYZ789", "Golf", "Volkswagen", "Red", "Hatchback"),
        new Motorcycle("MOC111", "Ninja", "Kawasaki", "Black", "Sports"),
        new Bus("BUS222", "CityBus", "Volvo", "White", "DoubleDecker"),
        new Boat("BOA333", "Oceania", "Yamaha", "Blue", "Yacht"),
        new Airplane("ZOZKAK", "Raiju", "Dusan", "Black", "Fighter")
    };

            int successCount = 0;
            foreach (var v in testVehicles)
            {
                if (ParkVehicle(v))
                {
                    successCount++;
                }
            }

            Console.WriteLine($"\nSuccessfully seeded {successCount} test vehicles into the garage!");
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            if ( _myGarage == null) return Enumerable.Empty<Vehicle>();

            return _myGarage;
        }
    }
}
