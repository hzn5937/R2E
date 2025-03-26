using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssignmentD1
{
    public class Garage
    {
        public List<Car> Cars { get; set; }

        public Garage()
        {
            Cars = new List<Car>();
            this.InitializeGarage();
        }

        public void InitializeGarage()
        {
            Cars.Add(new Car("Tesla", "Model S", 2021, Car.Type.Electric));
            Cars.Add(new Car("Toyota", "Corolla", 2021, Car.Type.Fuel));
            Cars.Add(new Car("Ford", "F-150", 2021, Car.Type.Fuel));
            Cars.Add(new Car("Chevrolet", "Bolt", 2021, Car.Type.Electric));
            Cars.Add(new Car("Nissan", "Leaf", 2021, Car.Type.Electric));
            Cars.Add(new Car("Tesla", "Model 3", 2021, Car.Type.Electric));
            Cars.Add(new Car("Tesla", "Model X", 2021, Car.Type.Electric));
        }

        public void PrintCars(List<Car> cars)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Join(Environment.NewLine, 
                cars.Select(car => $"Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Type: {car.CarType}")));

            Console.WriteLine(sb.ToString());
        }

        public void DisplayAll()
        {
            Console.WriteLine("-------------------------------------------------");
            this.PrintCars(Cars);
        }

        public void AddCar()
        {
            Console.WriteLine("-------------------------------------------------");
            try
            {
                Console.WriteLine("Enter Car Type (Electric/Fuel): ");
                string? carTypeInput = Console.ReadLine();

                Car.Type carType;

                // Validate car type by parsing and checking if it's defined in the enum
                while (!Enum.TryParse<Car.Type>(carTypeInput, true, out carType) || !Enum.IsDefined(typeof(Car.Type), carType))
                {
                    Console.WriteLine("Invalid Type, Please Enter Electric or Fuel: ");
                    carTypeInput = Console.ReadLine();
                }

                Console.WriteLine("Enter Make: ");
                string? makeInput = Console.ReadLine();

                while (makeInput == string.Empty)
                {
                    Console.WriteLine("Please Enter Car Make: ");
                    makeInput = Console.ReadLine();
                }

                Console.WriteLine("Enter Model: ");
                string? modelInput = Console.ReadLine();

                while (modelInput == string.Empty)
                {
                    Console.WriteLine("Please Enter Car Model: ");
                    modelInput = Console.ReadLine();
                }

                Console.WriteLine("Enter Year");
                string? yearInput = Console.ReadLine();

                //todo: check regex
                while (yearInput == string.Empty || !Regex.IsMatch(yearInput, @"^\d{4}$"))
                {
                    Console.WriteLine("Please Enter A Valid Year: ");
                    yearInput = Console.ReadLine();
                }

                int year = int.Parse(yearInput);

                Car newCar = new Car(makeInput, modelInput, year, carType);
                Cars.Add(newCar);

                Console.WriteLine("Car added successfully!");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");    
            }
        }

        public void RemoveCarByModel()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Enter Model: ");
            string? modelInput = Console.ReadLine();

            Car? removingCar = Cars.FirstOrDefault(c => c.Model.Equals(modelInput, StringComparison.OrdinalIgnoreCase));

            if (removingCar == null)
            {
                Console.WriteLine("Car not found!");
                return;
            }

            Cars.Remove(removingCar);
            Console.WriteLine("Car removed successfully!");
        }

        public void SearchCarByMake()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Enter Make: ");
            string? makeInput = Console.ReadLine();

            var result = Cars.Where(c => c.Make.Contains(makeInput, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Search Results: ");
            this.PrintCars(result.ToList());
        }

        public void FilterCarsByType()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Enter Car Type (Electric/Fuel): ");
            string? carTypeInput = Console.ReadLine();
            Car.Type carType;

            while (!Enum.TryParse<Car.Type>(carTypeInput, true, out carType) || !Enum.IsDefined(typeof(Car.Type), carType))
            {
                Console.WriteLine("Please Enter Valid Type: ");
                carTypeInput = Console.ReadLine();
            }

            var result = Cars.Where(c => c.CarType.Equals(carType));

            Console.WriteLine("Search Results: ");
            this.PrintCars(result.ToList());
        }
    }
}
