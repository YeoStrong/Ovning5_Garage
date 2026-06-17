using Ovning5_Garage.Core;
using Ovning5_Garage.Interfaces;
using Ovning5_Garage.Vehicles;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Ovning5_Garage.UI
{
    public class ConsoleUI : IConsoleUI
    {
        private GarageHandler _handler = new GarageHandler();

        public void Run()
        {
            _handler.CreateGarage(100);

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("====== Garage Management Program =====");
                Console.WriteLine("1. View Garage Status");
                Console.WriteLine("2. Park Vehicle");
                Console.WriteLine("3. Retrieve Vehicle");
                Console.WriteLine("4. Search Vehicle");
                Console.WriteLine("5. Save Garage Data");
                Console.WriteLine("6. Load Garage Data");
                Console.WriteLine("7. Put Seed Data");
                Console.WriteLine("8. Garage Detail Overview");
                Console.WriteLine("0. Exit Program");
                Console.WriteLine("======================================");
                Console.Write("Enter the menu number: ");
                string input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        string status = _handler.GarageStatus();
                        Console.WriteLine("\n[Garage Status]");
                        Console.WriteLine(status);
                        Console.WriteLine("\nPush any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        ParkVehicleMenu();
                        break;

                    case "3":
                        string regNr = ReadValidString("\nEnter the register number of car to retrieve: ");

                        bool isRemoved = _handler.RemoveVehicle(regNr);
                        if (isRemoved)
                        {
                            Console.WriteLine($"\nRegister number {regNr.ToUpper()} has been retrieved.");
                        }
                        else
                        {
                            Console.WriteLine($"\nCan't find register number {regNr}.");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                        SearchVehicleMenu();
                        break;

                    case "5":
                        Console.WriteLine("\nSaving Garage Data.");
                        bool isSaved = _handler.SaveGarageData();

                        if (isSaved)
                        {
                            Console.WriteLine("\nData has been saved.");
                        }
                        else
                        {
                            Console.WriteLine("\nCould not save data...");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.WriteLine("\nLoading Garage Data.");
                        bool isLoaded = _handler.LoadGarageData();
                        if (isLoaded)
                        {
                            Console.WriteLine("\nData has been loaded.");
                        }
                        else
                        {
                            Console.WriteLine("\nCould not load data...");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "7":
                        _handler.SeedData();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "8":
                        Console.Clear();
                        Console.WriteLine("\n===== Garage Detail Overview =====");

                        var allVehicles = _handler.GetVehicles();

                        int totalCount = 0;
                        if (allVehicles != null)
                        {
                            foreach (var v in allVehicles)
                            {
                                if (v != null)
                                {
                                    totalCount++;
                                    Console.WriteLine(v.ToString());
                                }
                            }
                        }

                        if (totalCount == 0)
                        {
                            Console.WriteLine("\nThe garage is completely empty!");
                        }
                        else
                        {
                            Console.WriteLine($"\nTotal parked vehicles: {totalCount}");
                        }

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("\nExiting the program, See you next time!");
                        break;

                    default:
                        Console.WriteLine("\nWrong input. Please choose the right number.\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SearchVehicleMenu()
        {
            Console.Clear();
            Console.WriteLine("===== Search Vehicle =====");
            Console.WriteLine("1. Search by Register Number");
            Console.WriteLine("2. Search by Color");
            Console.WriteLine("3. Search by Register Number & Color");
            string searchOption = ReadValidString("Choose search option: ");

            IEnumerable<Vehicle> results = Enumerable.Empty<Vehicle>();

            switch (searchOption)
            {
                case "1":
                    string regInput = ReadValidString("\nEnter Register Number  of it(or part if you don't remember well): ").ToUpper();
                    results = _handler.SearchVehicles(v => v.RegNr.Contains(regInput));
                    break;

                case "2":
                    string colorInput = ReadValidString("\nEnter Color to search: ").ToUpper();
                    results = _handler.SearchVehicles(v => v.Color.ToUpper() == colorInput);
                    break;

                case "3":
                    regInput = ReadValidString("\nEnter Register Number  of it(or part if you don't remember well): ").ToUpper();
                    colorInput = ReadValidString("\nEnter Color to search: ");
                    results = _handler.SearchVehicles(v => v.RegNr.Contains(regInput) && v.Color.ToUpper() == colorInput);
                    break;

                default:
                    Console.WriteLine("\nInvalid option!");
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                    return;
            }

            Console.WriteLine("\n===== Search Results =====");

            int matchCount = 0;
            foreach (var v in results)
            {
                matchCount++;
                Console.WriteLine($"[{v.GetType().Name}] RegNr: {v.RegNr} | Model: {v.Name} | Brand: {v.Brand} | Color: {v.Color}");
            }

            if (matchCount == 0)
            {
                Console.WriteLine("No matching vehicles found.");
            }
            else
            {
                Console.WriteLine($"\nFound {matchCount} vehicle(s).");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ParkVehicleMenu()
        {
            Console.Clear();
            Console.WriteLine("===== Park Vehicle =====");
            Console.WriteLine("Select Vehicle Type");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Airplane");
            Console.WriteLine("3. Motorcycle");
            Console.WriteLine("4. Bus");
            Console.WriteLine("5. Boat");
            string typeInput = ReadValidString("Enter the type number: ");

            string regNr = ReadValidString("Enter Register Number: ");

            string name = ReadValidString("Enter Model Name: ");

            string brand = ReadValidString("Enter Brand: ");

            string color = ReadValidString("Enter Color: ");

            bool isParked = false;

            switch (typeInput)
            {
                case "1":
                    string carType = ReadValidString("Enter Car Type: ");

                    Car newCar = new Car(regNr, name, brand, color, carType);
                    isParked = _handler.ParkVehicle(newCar);
                    break;

                case "2":
                    string apType = ReadValidString("Enter Airplane Type: ");

                    Airplane newAp = new Airplane(regNr, name, brand, color, apType);
                    isParked = _handler.ParkVehicle(newAp);
                    break;

                case "3":
                    string mcType = ReadValidString("Enter Motorcycle Type: ");

                    Motorcycle newMc = new Motorcycle(regNr, name, brand, color, mcType);
                    isParked = _handler.ParkVehicle(newMc);
                    break;

                case "4":
                    string busType = ReadValidString("Enter Bus Type: ");

                    Bus newBus = new Bus(regNr, name, brand, color, busType);
                    isParked = _handler.ParkVehicle(newBus);
                    break;

                case "5":
                    string boatType = ReadValidString("Enter Boat Type: ");

                    Boat newBoat = new Boat(regNr, name, brand, color, boatType);
                    isParked = _handler.ParkVehicle(newBoat);
                    break;

                default:
                    Console.WriteLine("\nInvalid vehicle type!");
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                    return;
            }

            if (isParked)
            {
                Console.WriteLine($"\nSuccessfully parked the vehicle [{regNr.ToUpper()}]");
            }
            else
            {
                Console.WriteLine($"\nParking failed! The garage might be full or the register number [{regNr.ToUpper()}] already exists.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private string ReadValidString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("You cannot input empty or only whitespace.");
            }
        }
    }
}
