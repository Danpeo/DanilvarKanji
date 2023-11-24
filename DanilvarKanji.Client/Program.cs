using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanilvarKanji.Client;
using DanilvarKanji.Client.Extensions;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Domain.DTO;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7106/") });

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseQueryService<CharacterDto>, BaseQueryService<CharacterDto>>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

//await builder.Build().RunAsync();

WebAssemblyHost host = builder.Build();
await host.SetDefaultCulture(); 
await host.RunAsync();