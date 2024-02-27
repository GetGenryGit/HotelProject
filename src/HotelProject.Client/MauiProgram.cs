using Client.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace HotelProject.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddClientServices();
            builder.Services.AddManagers();
            builder.Services.AddHttpClient("Hotel.API", client =>
             {
                 client.BaseAddress = new Uri
                 (
#if DEBUG
                     "http://10.0.2.2:5258"
#else
                    "release"
#endif
                 );
             });

            return builder.Build();
        }
    }
}
