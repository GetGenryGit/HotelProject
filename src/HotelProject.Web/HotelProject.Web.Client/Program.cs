using Client.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddClientServices();
builder.Services.AddManagers();
builder.Services.AddHttpClient("Hotel.API", client =>
{
    client.BaseAddress = new Uri
    (
#if DEBUG
        "http://localhost:5258"
#else
                    "release"
#endif
    );
});

await builder.Build().RunAsync();
