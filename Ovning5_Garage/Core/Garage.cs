using Ovning5_Garage.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Core
{
    public class Garage<T> : IEnumerable<T>, IGarage<T> where T : Vehicle
    {
        private T[] _vehicles;
        public int Capacity { get; private set; }
        public double CurrentUsedSize => _vehicles.Where(v => v != null).Sum(v => v.Size);

        public Garage(int capacity)
        {
            Capacity = capacity;
            _vehicles = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var vehicle in _vehicles)
            {
                if (vehicle is not null)
                    yield return vehicle;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Park(T vehicle)
        {
            if (CurrentUsedSize + vehicle.Size > Capacity)
            {
                return false;
            }
            foreach (var existVehicle in _vehicles)
            {

                if (existVehicle != null && existVehicle.RegNr == vehicle.RegNr)
                {
                    return false;
                }
            }
            for (var i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] == null)
                {
                    _vehicles[i] = vehicle;
                    return true;
                }
            }
            return false;
        }

        public bool Remove(string regNr)
        {
            for (var i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] != null && _vehicles[i].RegNr == regNr.ToUpper())
                {
                    _vehicles[i] = null;
                    return true;
                }
            }
            return false;
        }

        public string GetVehicleCounts()
        {
            int carCount = 0;
            int airplaneCount = 0;
            int motorcycleCount = 0;
            int busCount = 0;
            int boatCount = 0;

            foreach (var vehicle in _vehicles)
            {
                if (vehicle == null) continue;
                if (vehicle is Car) carCount++;
                if (vehicle is Airplane) airplaneCount++;
                if (vehicle is Motorcycle) motorcycleCount++;
                if (vehicle is Bus) busCount++;
                if (vehicle is Boat) boatCount++;
            }

            string result = $"Car: {carCount}\nAirplane: {airplaneCount}\nMotorcycle: {motorcycleCount}\nBus: {busCount}\nBoat: {boatCount}\n";

            return result;
        }

        public IEnumerable<T> Search(Func<T, bool> predicate)
        {
            return this.Where(v => v != null).Where(predicate);
        }
    }
}
