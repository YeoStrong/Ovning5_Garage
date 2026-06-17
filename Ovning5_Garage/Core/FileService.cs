using Ovning5_Garage.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ovning5_Garage.Core
{
    public class FileService
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
        };

        public static void SaveGarage(string filePath, Vehicle[] vehicles)
        {
            string jsonString = JsonSerializer.Serialize(vehicles, _options);

            File.WriteAllText(filePath, jsonString);
        }

        public static Vehicle[] LoadGarage(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return Array.Empty<Vehicle>();
            }

            string jsonString = File.ReadAllText(filePath);

            Vehicle[] vehicles = JsonSerializer.Deserialize<Vehicle[]>(jsonString, _options);

            return vehicles;
        }
    }
}
