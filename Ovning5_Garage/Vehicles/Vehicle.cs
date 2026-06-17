using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Ovning5_Garage.Vehicles
{
    [JsonDerivedType(typeof(Car), typeDiscriminator: "Car")]
    [JsonDerivedType(typeof(Airplane), typeDiscriminator: "Airplane")]
    [JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "Motorcycle")]
    [JsonDerivedType(typeof(Bus), typeDiscriminator: "Bus")]
    [JsonDerivedType(typeof(Boat), typeDiscriminator: "Boat")]
    public class Vehicle
    {
        private string _regNr;

        public Vehicle(string regNr, string name, string brand, string color)
        {
            RegNr = regNr;
            Name = name;
            Brand = brand;
            Color = color;
        }
        public string RegNr
        { 
            get { return _regNr; }
            set { _regNr = value.ToUpper(); }
        }

        public string Name { get; init; }
        public string Brand { get; init; }
        public string Color { get; init; }
        public virtual double Size { get; } = 1.0;

        
    }
}
