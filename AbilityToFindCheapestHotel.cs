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
         choices();
     }
      public static void choices()
     {
     Console.WriteLine("Choose your choice for operation");
     Console.WriteLine("1. Add hotel name and Regular price of hotel.\n" +
                       "2. Find the cheapest hotel for a given Date Range with Regular price.");
      int choice = Convert.ToInt32(Console.ReadLine());
      switch (choice)
      {
      case 1:
        addHotelWithRegularPrice();
        choices();
        break;
      case 2:
        Cheapest_hotel_InGivenDates();
        choices();
        break;
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
   private static void Cheapest_hotel_InGivenDates()
   {
     Console.WriteLine("Enter the start date: ");
     DateTime startdate = DateTime.Parse(Console.ReadLine());
     Console.WriteLine("Enter the End Date");
     DateTime enddate = DateTime.Parse(Console.ReadLine());
     uint totalRate = 0;
     var hotel = (from val in hotels orderby val.WeekdayRegularRate select val).First();
     for (DateTime i = startdate; i <= enddate; i = i.AddDays(1))
     {
         totalRate += hotel.WeekdayRegularRate;
     }
     Console.WriteLine($"Hotel name is:{hotel.Name} , Total Rate : ${totalRate}");
   }
}
