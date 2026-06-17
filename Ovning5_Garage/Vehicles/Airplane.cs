using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Vehicles
{
    public class Airplane : Vehicle
    {
        public Airplane(string regNr, string name, string brand, string color, string apType) : base(regNr, name, brand, color)
        {
            ApType = apType;
        }

        public string ApType { get; set; }
        public override double Size => 20.0;
    }
}
