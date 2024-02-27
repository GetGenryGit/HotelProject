using HotelProject.Infrastructure.Extenstions;
using HotelProject.Application.Extensions;
using HotelProject.Web.Extenstions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var configurations = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDatabase(configurations);

builder.Services.AddRepositories();
builder.Services.AddHelpers();
builder.Services.AddServices();

builder.Services.AddJWTAuthentication(configurations);
builder.Services.AddSwagger();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials


app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<Server.Web.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Client.WebComponents._Imports).Assembly);

app.Run();
