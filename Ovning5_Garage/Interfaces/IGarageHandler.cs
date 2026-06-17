using Ovning5_Garage.Vehicles;

namespace Ovning5_Garage.Interfaces
{
    public interface IGarageHandler
    {
        void CreateGarage(int capacity);
        string GarageStatus();
        bool ParkVehicle(Vehicle vehicle);
        bool RemoveVehicle(string regNr);
        IEnumerable<Vehicle> SearchVehicles(Func<Vehicle, bool> predicate);
    }
}