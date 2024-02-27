namespace Client.Infrastructure.Routes.Bookings;

public static class BookingStatusEndpoints
{
    public static string GetAll = "api/booking/status"; // get
    public static string Add = "api/booking/status"; // post

    public static string Update(Guid id)
    {
        return $"api/booking/status/{id}";
    }

    public static string Delete(Guid id)
    {
        return $"api/booking/status/{id}";
    }

}
