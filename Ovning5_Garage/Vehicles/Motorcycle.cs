using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string regNr, string name, string brand, string color, string mcType) : base(regNr, name, brand, color)
        {
            McType = mcType;
        }

        public string McType { get; set; }
        public override double Size => 1.0 / 3.0;

        public override string ToString()
        {
            return base.ToString() + $" | Motorcycle Type: {McType}";
        }
    }
}
