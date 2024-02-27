using Blazored.LocalStorage;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Routes.Identity;
using HotelProject.Application.Requests.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Constants.Storage;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Net.Http.Headers;
using Client.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components;
using Client.Infrastructure.Services;

namespace Client.Infrastructure.Managers.AuthenticationManager;

public class AuthenticationManager(IHttpService httpService,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider,
    NavigationManager navigationManager)
    : IAuthenticationManager
{
    private readonly IHttpService _httpService = httpService;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
    private readonly NavigationManager _navigationManager = navigationManager;

    public async Task<ClaimsPrincipal> CurrentUser()
    {
        var state = await _authStateProvider.GetAuthenticationStateAsync();
        return state.User;
    }

    public async Task<IResult> Login(TokenRequest model)
    {
        var response = await _httpService.Post(TokenEndpoints.Get, model);
        var result = await response.ToResult<TokenResponse>();
        if (result.Succeeded)
        {
            var authToken = result.Data.AuthToken;
            var refreshToken = result.Data.RefreshToken;

            await _localStorage.SetItemAsync(StorageConstants.AuthToken, authToken);
            await _localStorage.SetItemAsync(StorageConstants.RefreshToken, refreshToken);

            await ((HotelStateProvider)_authStateProvider).StateChangedAsync();

            return await Result
                .SuccessAsync();
        }
        else
        {
            return await Result
                .FailAsync(result.Messages);
        }
    }

    public async Task<IResult> Logout()
    {
        await _localStorage.RemoveItemAsync(StorageConstants.AuthToken);
        await _localStorage.RemoveItemAsync(StorageConstants.RefreshToken);
        ((HotelStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        _navigationManager.NavigateTo("/home", true, true);

        return await Result
            .SuccessAsync("Пользователь успешно закрыл сессию!");
    }

}
