using System.Globalization;
using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace AssignmentD2
    {
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter car make: ");
                string? make = Console.ReadLine();
                
                while (string.IsNullOrEmpty(make))
                {
                    Console.WriteLine("Please enter a valid car make: ");
                    make = Console.ReadLine();
                }

                Console.Write("Enter car model: ");
                string? model = Console.ReadLine();

                while (string.IsNullOrEmpty(model))
                {
                    Console.WriteLine("Please enter a valid car model: ");
                    model = Console.ReadLine();
                }

                Console.Write("Enter car year (e.g., 2020): ");
                string? yearInput = Console.ReadLine();
                int year;
                int currentYear = DateTime.Now.Year;

                // TryParse return true if the conversion succeeded 
                while (!int.TryParse(yearInput, out year) || 
                    !WithinValidYear(year, 1886, currentYear))
                {
                    Console.WriteLine("Invalid year! Please enter a valid year between 1886 and the current year");
                    Console.Write("Enter car year (e.g., 2020): ");
                    yearInput = Console.ReadLine();
                }

                Console.Write("Enter last maintenance date (yyyy-MM-dd): ");
                string? lastMaintenanceDateInput = Console.ReadLine();
                DateTime lastMaintenanceDate;

                while (!DateTime.TryParseExact(lastMaintenanceDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out lastMaintenanceDate) ||
                    !WithinValidYear(lastMaintenanceDate.Year, year, currentYear))
                {
                    Console.WriteLine("Invalid date format! Please enter a valid date");
                    Console.Write("Enter last maintenance date (yyyy-MM-dd): ");
                    lastMaintenanceDateInput = Console.ReadLine();
                }

                Console.Write("Is this a FuelCar or ElectricCar? (F/E): ");
                string? carType = Console.ReadLine();

                //while (!carType.Equals("f", StringComparison.OrdinalIgnoreCase) 
                //    && !carType.Equals("e", StringComparison.OrdinalIgnoreCase))

                while (!IsValidOption(carType, new string[] { "F", "E" }))
                {
                    Console.WriteLine(@"Invalid input! Please enter 'F' for FuelCar or 'E' for ElectricCar");
                    Console.Write("Is this a FuelCar or ElectricCar? (F/E): ");
                    carType = Console.ReadLine();
                }

                Car car = carType.Equals("e", StringComparison.OrdinalIgnoreCase) ? 
                    new ElectricCar(make, model, year, lastMaintenanceDate) : 
                    new FuelCar(make, model, year, lastMaintenanceDate);

                car.DisplayDetails();

                Console.Write("Do you want to refuel/charge? (Y/N): ");
                string? refuelOrCharge = Console.ReadLine();

                //while (!refuelOrCharge.Equals("y", StringComparison.OrdinalIgnoreCase) &&
                //    !refuelOrCharge.Equals("n", StringComparison.OrdinalIgnoreCase))

                while(!IsValidOption(refuelOrCharge, new string[] { "Y", "N" }))
                {
                    Console.WriteLine(@"Invalid input! Please enter 'Y' to refuel/charge or 'N' to skip");
                    Console.Write("Do you want to refuel/charge? (Y/N): ");
                    refuelOrCharge = Console.ReadLine();
                }

                if (refuelOrCharge.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                    
                Console.Write("Enter refuel/charge date and time (yyyy-MM-dd HH:mm): ");
                string? dateTimeString = Console.ReadLine();
                DateTime fuelOrChargeDateTime;

                while (!DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out fuelOrChargeDateTime) ||
                    !WithinValidYear(fuelOrChargeDateTime.Year, year, currentYear))
                {
                    Console.WriteLine("Invalid date format! Please enter a valid date.");
                    Console.Write("Enter refuel/charge date and time (yyyy-MM-dd HH:mm): ");
                    dateTimeString = Console.ReadLine();
                }

                DateTime dateTime = DateTime.Parse(dateTimeString);

                if (car is FuelCar fuelCar)
                {
                    fuelCar.Refuel(dateTime);
                }
                else
                {
                    ElectricCar electricCar = (ElectricCar)car;
                    electricCar.Charge(dateTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
        static bool WithinValidYear(int yearInput, int year, int currentYear)
        {
            return yearInput >= year && yearInput <= currentYear;
        }

        static bool IsValidOption(string option, string[] validOptions)
        {
            return validOptions.Contains(option.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}