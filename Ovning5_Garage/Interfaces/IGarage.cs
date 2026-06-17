using Ovning5_Garage.Vehicles;

namespace Ovning5_Garage
{
    public interface IGarage<T> where T : Vehicle
    {
        int Capacity { get; }
        double CurrentUsedSize { get; }

        IEnumerator<T> GetEnumerator();
        string GetVehicleCounts();
        bool Park(T vehicle);
        bool Remove(string regNr);
        IEnumerable<T> Search(Func<T, bool> predicate);
    }
}