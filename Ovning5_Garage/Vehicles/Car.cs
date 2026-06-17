using System;
using System.Collections.Generic;
using System.Text;

namespace Ovning5_Garage.Vehicles
{
    public class Car : Vehicle
    {
        public Car(string regNr, string name, string brand, string color, string carType) : base(regNr, name, brand, color)
        {
            CarType = carType;
        }

        public string CarType { get; set; }
        public override double Size => 1.0;

        public override string ToString()
        {
            return base.ToString() + $" | Car Type: {CarType}";
        }
    }
}
