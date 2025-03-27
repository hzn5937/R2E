using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentD2
{
    public abstract class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime LastMaintenanceDate { get; set; }

        public Car(string make, string model, int year, DateTime lastMaintenanceDate)
        {
            Make = make;
            Model = model;
            Year = year;
            LastMaintenanceDate = lastMaintenanceDate;
        }

        public DateTime NextMaintenanceDate()
        {
            return LastMaintenanceDate.AddMonths(6);
        }

        public void DisplayDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Car: {Make} {Model} ({Year})");
            sb.AppendLine($"Last maintenance date: {LastMaintenanceDate:yyyy-MM-dd}");
            sb.AppendLine($"Next maintenance date: {NextMaintenanceDate():yyyy-MM-dd}");
            Console.WriteLine(sb.ToString());
        }
    }
}
