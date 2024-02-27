namespace Client.Infrastructure.Routes.Identity;

public static class TokenEndpoints
{
    public static string Get = "api/identity/token"; // post
    public static string Revoke = "api/identity/token/revoke"; // post
    public static string Refresh = "api/identity/token/refresh"; // put

}
