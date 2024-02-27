using HotelProject.Application.Requests.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using Shared.Constants.Storage;
using System.Net.Http.Json;
using Client.Infrastructure.Routes.Identity;
using Client.Infrastructure.Managers.AuthenticationManager;
using System.Text;
using HotelProject.Application.Responses.Identity;
using Client.Infrastructure.Authentication;
using Client.Infrastructure.Extensions;

namespace Client.Infrastructure.Services;

public class HttpService(HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider,
    NavigationManager navigationManager)
    : IHttpService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
    private readonly NavigationManager _navigationManager = navigationManager;
    public async Task<HttpResponseMessage> Delete(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> Get(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> Post(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest(request);
    }
    public async Task<HttpResponseMessage> Patch(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> Put(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest(request);
    }

    private async Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
    {
        // get jwt access token from storage
        var authToken = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);
        if (authToken != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        var response = await _httpClient.SendAsync(request);
        // user didnt authorized or access token is expired
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.RefreshToken);

            using var responseRefreshAccessToken = await _httpClient.PutAsJsonAsync(TokenEndpoints.Refresh, 
                new RefreshTokenRequest 
                {
                    AuthToken = authToken,
                    RefreshToken = refreshToken
                });

            // refresh token is expired, redirect authorized
            if (responseRefreshAccessToken.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _localStorage.RemoveItemAsync(StorageConstants.AuthToken);
                await _localStorage.RemoveItemAsync(StorageConstants.RefreshToken);
                ((HotelStateProvider)_authStateProvider).MarkUserAsLoggedOut();
                _navigationManager.NavigateTo("/login", false, false);
                return default;
            }

            var refreshTokenResponse = await responseRefreshAccessToken.ToResult<TokenResponse>();

            await _localStorage.SetItemAsync(StorageConstants.AuthToken, refreshTokenResponse.Data.AuthToken);
            await _localStorage.SetItemAsync(StorageConstants.RefreshToken, refreshTokenResponse.Data.RefreshToken);

            var customAuthenticationStateProvider = (HotelStateProvider)_authStateProvider;
            await customAuthenticationStateProvider.StateChangedAsync();


            var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);
            newRequest.Content = request.Content;
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshTokenResponse.Data.AuthToken);

            response = await _httpClient.SendAsync(newRequest);
        }

        return response;
    }
}
