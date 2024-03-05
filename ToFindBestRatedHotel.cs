class Hotel
    {
        public string Name { get; set; }
        public uint WeekdayRegularRate { get; set; }
        public uint WeekendRegularRate { get; set; }
        public uint Rating { get; set; }
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
                          "2. Find the cheapest hotel for a given Date Range with Regular price.\n" +
                          "3. Add  weekday and weekend Rates.\n" +
                          "4. Find the cheapest hotel for a given Date Range with weekday and weekend price.\n" +
                          "5. Ability to Add Ratings to the Hotel.\n"+
                          "6. Ability to find Cheapest best rated hotel.\n"+
                          "7. Ability to find Best Rated Hotel\n"+
                          "8. Exit the program.");
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
    case 3:
        AddWeekDayAndRegularRates();
        choices();
        break;
    case 4:
        Cheapest_hotel_InGivenDatesWithWeekend();
        choices();
        break;
    case 5:
        Ability_To_AddRatings();
        choices();
        break;
    case 6:
        Cheapest_BestRated_Hotel();
        choices();
        break;
    case 7:
        BestRated_Hotel();
        choices();
        break;
    case 8:
            break;
        default:
            Console.WriteLine("Invalid Choice");
            break;
    }
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
private static void AddWeekDayAndRegularRates()
{
   
            for (int i = 0; i < hotels.Count; i++)
            {
               
                    Console.WriteLine("Enter the weekend rate for the hotel : "+ hotels[i].Name);
                    hotels[i].WeekendRegularRate = Convert.ToUInt32(Console.ReadLine());
                    Console.WriteLine("Enter the weekday rate for the hotel : " + hotels[i].Name);
                    hotels[i].WeekdayRegularRate = Convert.ToUInt32(Console.ReadLine());
                    Console.WriteLine("Added Successfully.");
            }  
}
private static void Cheapest_hotel_InGivenDatesWithWeekend()
{
    Console.WriteLine("Enter the start date: ");
    DateTime startdate = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Enter the End Date");
    DateTime enddate = DateTime.Parse(Console.ReadLine());
    uint minRate = uint.MaxValue;
    var availableHotels = hotels.Select(hotel =>
    {
        uint totalRate = 0;
        for (DateTime i = startdate; i <= enddate;)
        {
            if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
            {
                totalRate += hotel.WeekendRegularRate;
            }
            else
            {
                totalRate += hotel.WeekdayRegularRate;
            }
            i = i.AddDays(1);
        }
        minRate = Math.Min(minRate,totalRate);
        return new { hotel.Name, totalRate };
    });
    var result = from val in availableHotels where val.totalRate == minRate select val;
    foreach(var res  in result)
    {
        Console.WriteLine(res.Name+", ");
    }
    Console.WriteLine($"Total Rate : ${minRate}");
}
public static void Ability_To_AddRatings()
{
    for(int i = 0;i<hotels.Count;i++) 
    {
        Console.WriteLine("Enter the Rating for the hotel " + hotels[i].Name);
        hotels[i].Rating=Convert.ToUInt32(Console.ReadLine());
    }
}
public static void Cheapest_BestRated_Hotel()
{
    Console.WriteLine("Enter the start date: ");
    DateTime startdate = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Enter the End Date");
    DateTime enddate = DateTime.Parse(Console.ReadLine());
    var availableHotels = hotels.Select(hotel =>
    {
        uint totalRate = Cheapest( startdate, enddate, hotel);
        return new { hotel.Name, hotel.Rating, totalRate };
    });
    var cheapestHotel = availableHotels.OrderBy(hotel => hotel.totalRate)
                                        .ThenByDescending(hotel => hotel.Rating)
                                        .First();
    Console.WriteLine($"{cheapestHotel.Name}, Rating:{cheapestHotel.Rating} Total Rates: ${cheapestHotel.totalRate}");
}
public static uint Cheapest( DateTime start, DateTime end, Hotel hotel)
{
    uint totalRate = 0;
    for (DateTime i = start; i <= end;)
    {
        if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
        {
            totalRate += hotel.WeekendRegularRate;
        }
        else
        {
            totalRate += hotel.WeekdayRegularRate;
        }
        i = i.AddDays(1);
    }
    return totalRate;
}
 public static void BestRated_Hotel()
 {
     Console.WriteLine("Enter the start date: ");
     DateTime startdate = DateTime.Parse(Console.ReadLine());
     Console.WriteLine("Enter the End Date");
     DateTime enddate = DateTime.Parse(Console.ReadLine());
     var availableHotels = hotels.Select(hotel =>
     {
         uint totalRate = Cheapest(startdate, enddate, hotel);
         return new { hotel.Name, hotel.Rating, totalRate };
     });
     var BestRate = availableHotels.OrderByDescending(hotel => hotel.totalRate).
                                          ThenByDescending(hotel => hotel.Rating).First();
     Console.WriteLine($"{BestRate.Name}, Total Rates: ${BestRate.totalRate}");

 }
}
