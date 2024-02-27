using Blazored.LocalStorage;
using Shared.Constants.Storage;
using System.Net.Http.Headers;

namespace Client.Infrastructure.Authentication;

public class AuthenticationHeaderHandler(ILocalStorageService localStorage)
    : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage = localStorage;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization?.Scheme != "Bearer")
        {
            var savedToken = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);

            if (!string.IsNullOrEmpty(savedToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
