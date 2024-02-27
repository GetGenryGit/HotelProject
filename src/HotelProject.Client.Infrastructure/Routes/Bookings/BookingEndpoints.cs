
namespace Client.Infrastructure.Routes.Bookings;

public static class BookingEndpoints
{
    public static string GetAll = "api/booking"; // get
    public static string Add = "api/booking"; // post

    public static string GetByUserId(string userId)  // get
    {
        return $"api/booking/user/{userId}";
    }

    public static string Update(string bookingId) //  patch
    {
        return $"api/booking/{bookingId}";
    }

    internal static string GetByBookingId(string bookingId) // get
    {
        return $"api/booking/{bookingId}";
    }
}
