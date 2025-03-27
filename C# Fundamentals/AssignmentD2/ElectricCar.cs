using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentD2
{
    public class ElectricCar : Car, IChargable
    {
        public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate) : base(make, model, year, lastMaintenanceDate)
        {
        }

        public void Charge(DateTime timeOfCharge)
        {
            // {timeOfCharge:yyyy-MM-dd HH:mm}
            Console.WriteLine($"ElectricCar {Make} {Model} charged on {timeOfCharge:yyyy-MM-dd HH:mm}.");
        }
    }
}
