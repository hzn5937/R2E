using System.Globalization;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
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
                
                while (string.IsNullOrWhiteSpace(make))
                {
                    Console.Write("Please enter a valid car make: ");
                    make = Console.ReadLine();
                }

                Console.Write("Enter car model: ");
                string? model = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(model))
                {
                    Console.Write("Please enter a valid car model: ");
                    model = Console.ReadLine();
                }

                Console.Write("Enter car year (e.g., 2020): ");
                string? yearInput = Console.ReadLine();
                int year;
                int currentYear = DateTime.Now.Year;
                string? errorMessage;

                // TryParse return true if the conversion succeeded 
                while (!WithinValidYear(yearInput, 1886, currentYear, out year))
                {
                    Console.WriteLine("Invalid year! Please enter a valid year between 1886 and the current year");
                    Console.Write("Enter car year (e.g., 2020): ");
                    yearInput = Console.ReadLine();
                }

                Console.Write("Enter last maintenance date (yyyy-MM-dd): ");
                string? lastMaintenanceDateInput = Console.ReadLine();
                DateTime lastMaintenanceDate;

                while (!WithinValidYear(lastMaintenanceDateInput, "yyyy-MM-dd", year, currentYear, out lastMaintenanceDate, out errorMessage))
                {
                    Console.WriteLine(errorMessage);
                    Console.Write("Enter last maintenance date (yyyy-MM-dd): ");
                    lastMaintenanceDateInput = Console.ReadLine();
                }

                Console.Write("Is this a FuelCar or ElectricCar? (F/E): ");
                string? carType = Console.ReadLine();

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

                while (!WithinValidYear(dateTimeString, "yyyy-MM-dd HH:mm", year, currentYear, out fuelOrChargeDateTime, out errorMessage))
                {
                    Console.WriteLine(errorMessage);
                    Console.Write("Enter refuel/charge date and time (yyyy-MM-dd HH:mm): ");
                    dateTimeString = Console.ReadLine();
                }

                if (car is FuelCar fuelCar)
                {
                    fuelCar.Refuel(fuelOrChargeDateTime);
                }
                else
                {
                    ElectricCar electricCar = (ElectricCar)car;
                    electricCar.Charge(fuelOrChargeDateTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        static bool WithinValidYear(string? yearInput, int minYear, int maxYear, out int year)
        {
            return int.TryParse(yearInput, out year) && year >= minYear && year <= maxYear;
        }
        static bool WithinValidYear(string? dateInput, string format, int minYear, int maxYear, out DateTime date, out string? errorMessage)
        {
            if (!DateTime.TryParseExact(dateInput, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                errorMessage = $"Invalid date format! Please enter a date in the format {format}.";
                return false;
            }

            if (date.Year < minYear || date.Year > maxYear)
            {
                errorMessage = $"Invalid date! Please enter a date between {minYear} and {maxYear}.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        static bool IsValidOption(string option, string[] validOptions)
        {
            return validOptions.Contains(option.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}