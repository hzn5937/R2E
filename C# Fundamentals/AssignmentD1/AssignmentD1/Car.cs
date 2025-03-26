using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentD1
{
    public class Car
    {
        public enum Type
        {
            Electric,
            Fuel
        }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Type CarType { get; set; }

        public Car(string make, string model, int year, Type carType)
        {
            Make = make;
            Model = model;
            Year = year;
            CarType = carType;
        }
    }
}
