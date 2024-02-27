namespace Client.Infrastructure.Routes.Identity;

public static class RoleEndpoints
{
    public static string GetAll = "api/identity/role"; // get
    public static string Add = "api/identity/role"; // post
    public static string Delete(Guid userId) 
    {
        return $"api/identity/role/{userId}";
    }

    public static string Update(Guid userId)
    {
        return $"api/identity/role/{userId}";
    }

}
