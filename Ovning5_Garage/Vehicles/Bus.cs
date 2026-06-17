using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Vehicles
{
    public class Bus : Vehicle
    {
        public Bus(string regNr, string name, string brand, string color, string busType) : base(regNr, name, brand, color)
        {
            BusType = busType;
        }

        public string BusType { get; set; }
        public override double Size => 5.0;

        public override string ToString()
        {
            return base.ToString() + $" | Bus Type: {BusType}";
        }
    }
}
