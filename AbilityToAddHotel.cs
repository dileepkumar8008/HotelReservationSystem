 class Hotel
 {
     public string Name { get; set; }
     public uint WeekdayRegularRate { get; set; }
}
 class Program
 {
     private static List<Hotel> hotels = new List<Hotel>();
     public static void Main(string[] args)
     {
         Console.WriteLine("Welcome to Hotel Reservation Program");
           addHotelWithRegularPrice();
     }
 private static void addHotelWithRegularPrice()
 {

     while (true)
     {
         Console.WriteLine("Enter the Name of the Hotel:");
         string name = Console.ReadLine();
         if (hotels.Any(h => h.Name == name)) throw new Exception("Name already exists.");
         else
         {
             Console.WriteLine("Enter regular rate");
             uint Regular_price = Convert.ToUInt32(Console.ReadLine());
             Hotel t = new Hotel();
             t.Name = name;
             t.WeekdayRegularRate = Regular_price;
             hotels.Add(t);
             Console.WriteLine("Added Successfully.");
             break;
         }

     }
 }
}
