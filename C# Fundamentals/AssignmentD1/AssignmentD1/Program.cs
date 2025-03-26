namespace AssignmentD1
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage garage = new();

            while (true)
            {
                try
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("Menu: ");
                    Console.WriteLine("1. Add a Car ");
                    Console.WriteLine("2. View All Cars ");
                    Console.WriteLine("3. Search Cars By Make");
                    Console.WriteLine("4. Filter Cars By Type");
                    Console.WriteLine("5. Remove a Car By Model");
                    Console.WriteLine("6. Exit");

                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            garage.AddCar();
                            break;
                        case 2:
                            garage.DisplayAll();
                            break;
                        case 3:
                            garage.SearchCarByMake();
                            break;
                        case 4:
                            garage.FilterCarsByType();
                            break;
                        case 5:
                            garage.RemoveCarByModel();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}