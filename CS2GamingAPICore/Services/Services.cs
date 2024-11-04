using CounterStrikeSharp.API.Core;
using CS2GamingAPICore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CS2GamingAPICore.Services;

public class CS2GamingInjection : IPluginServiceCollection<CorePlugin>
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<ICS2GamingServices, CS2GamingHttpClient>();
        services.AddSingleton<CS2GamingHttpClient>();
        services.AddSingleton<HttpClient>();
    }
}
