namespace Client.Infrastructure.Routes.Rooms;

public static class RoomTypeEndpoints
{
    public static string GetAll = "api/room/type"; // get

    public static string Add = "api/room/type"; // post

    public static string Update(Guid typeId) // Patch
    {
        return $"api/room/type/{typeId}";
    }

    public static string Delete(Guid typeId) // Delete
    {
        return $"api/room/type/{typeId}";
    }
}
