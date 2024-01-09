using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.SessionStorage;
using Danilvar.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanilvarKanji.Client;
using DanilvarKanji.Client.Extensions;
using DanilvarKanji.Client.Handlers;
using DanilvarKanji.Client.JsWrapper;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Client.Services.Review;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Responses.Character;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7106/") });



builder.Services.AddHttpClient("ServerApi")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<AuthHandler>();
builder.Services.AddSingleton<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterLearningService, CharacterLearningService>();
builder.Services.AddScoped<IBaseQueryService<GetAllFromCharacterResponse>, BaseQueryService<GetAllFromCharacterResponse>>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<Js>();

builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddComponents();

builder.Services.AddBlazoredSessionStorageAsSingleton();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();

/*builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("ServerApi", options.ProviderOptions);
});*/

builder.Services.AddAuthorizationCore();

/*
builder.Services.AddApiAuthorization();
*/


//await builder.Build().RunAsync();

WebAssemblyHost host = builder.Build();
await host.SetDefaultCulture(); 
await host.RunAsync();

