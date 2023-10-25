using System.Globalization;
using Blazored.LocalStorage;
using DanilvarKanji.Client.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DanilvarKanji.Client.Extensions;

public static class WebAssemblyHostExtension
{
    public static async Task SetDefaultCulture(this WebAssemblyHost host)
    {
        var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
        var cultureString = await localStorage.GetItemAsync<string>("culture");

        CultureInfo cultureInfo;

        if (!string.IsNullOrWhiteSpace(cultureString))
        {
            cultureInfo = new CultureInfo(cultureString);
        }
        else
        {
            cultureInfo = new CultureInfo(LocalizerSettings.NeutralCulture.Name);
        }

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}