namespace Client.Infrastructure.Routes.Identity;

public static class UserEndpoints
{
    public static string GetAll = "api/identity/user"; // get
    public static string Register = "api/identity/user"; // post

    public static string GetUserById(Guid userId) // get
    {
        return $"api/identity/user/{userId}";
    }

    public static string GetUserRoleByUserId(Guid userId) // get
    {
        return $"api/identity/user/role/{userId}";
    }

    public static string PatchByUserId(Guid userId) // patch
    {
        return $"api/identity/user/{userId}";

    }
}
