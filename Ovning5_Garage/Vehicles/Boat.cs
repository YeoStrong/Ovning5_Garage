using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Vehicles
{
    public class Boat : Vehicle
    {
        public Boat(string regNr, string name, string brand, string color,string boatType) : base(regNr, name, brand, color)
        {
            BoatType = boatType;
        }

        public string BoatType { get; set; }
        public override double Size => 10.0;

        public override string ToString()
        {
            return base.ToString() + $" | Boat Type: {BoatType}";
        }
    }
}
