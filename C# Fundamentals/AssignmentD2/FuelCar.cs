using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentD2
{
    public class FuelCar : Car, IFuelable
    {
        public FuelCar(string make, string model, int year, DateTime lastMaintenanceDate) : base(make, model, year, lastMaintenanceDate)
        {
        }

        public void Refuel(DateTime timeOfRefuel)
        {
            Console.WriteLine($"FuelCar {Make} {Model} refueled on {timeOfRefuel:yyyy-MM-dd HH:mm}.");
        }
    }
}
