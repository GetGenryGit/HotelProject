namespace Client.Infrastructure.Routes.Rooms;

public static class RoomEndpoints
{
    public static string Add = "api/room"; // post
    public static string GetByDates(DateTime checkinDate, DateTime checkoutDate) // get
    {
        return $"api/room/{checkinDate:yyyy-MM-dd HH:mm}/{checkoutDate:yyyy-MM-dd HH:mm}";
    }
    public static string Get(string roomId) // Patch
    {
        return $"api/room/{roomId}";
    }

    public static string Update(Guid roomId) // Patch
    {
        return $"api/room/{roomId}";
    }

    public static string Delete(Guid roomId) // Delete
    {
        return $"api/room/{roomId}";
    } 
}
