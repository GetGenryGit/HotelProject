using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Constants.Storage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Client.Infrastructure.Authentication;

public class HotelStateProvider(HttpClient httpClient,
    ILocalStorageService localStorage)
    : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task StateChangedAsync()
    {
        var authState = Task.FromResult(await GetAuthenticationStateAsync());
        NotifyAuthenticationStateChanged(authState);
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));

        NotifyAuthenticationStateChanged(authState);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);
        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(getClaimsFromJwt(savedToken), "jwt")));
        return state;
    }

    private IEnumerable<Claim> getClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = parseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            keyValuePairs.TryGetValue("user_guid", out var userGuid);
            keyValuePairs.TryGetValue(ClaimTypes.NameIdentifier, out var userName);
            keyValuePairs.TryGetValue(ClaimTypes.Name, out var name);
            keyValuePairs.TryGetValue(ClaimTypes.Surname, out var surname);
            keyValuePairs.TryGetValue(ClaimTypes.Email, out var email);
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var role);
            keyValuePairs.TryGetValue(ClaimTypes.MobilePhone, out var mobilePhone);

            claims.Add(new Claim("user_guid", userGuid.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userName.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, name.ToString()));
            claims.Add(new Claim(ClaimTypes.Surname, surname.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, email.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            claims.Add(new Claim(ClaimTypes.MobilePhone, mobilePhone.ToString()));
        }
        return claims;
    }

    private byte[] parseBase64WithoutPadding(string payload)
    {
        payload = payload.Trim().Replace('-', '+').Replace('_', '/');
        var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        return Convert.FromBase64String(base64);
    }
}
