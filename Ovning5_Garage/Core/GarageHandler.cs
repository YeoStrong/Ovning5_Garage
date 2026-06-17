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
    }
}
