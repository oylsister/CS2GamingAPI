using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CS2GamingAPICore.Models;
using CS2GamingAPIShared;
using Microsoft.Extensions.Logging;

namespace CS2GamingAPICore;
public class CorePlugin : BasePlugin, IPluginConfig<APIConfigs>
{
    public override string ModuleName => "CS2Gaming API Core Plugin";
    public override string ModuleVersion => "1.0";

    public APIConfigs Config {  get; set; } = new APIConfigs();
    public readonly CS2GamingHttpClient _cs2GamingHttpClient;
    private readonly ILogger<CorePlugin> _logger;

    public CS2GamingAPI? API { get; set; }
    public static PluginCapability<ICS2GamingAPIShared> APICapability = new("cs2gamingAPI");

    public CorePlugin(CS2GamingHttpClient cs2GamingHttpClient, ILogger<CorePlugin> logger)
    {
        _cs2GamingHttpClient = cs2GamingHttpClient;
        _logger = logger;
    }

    public override void Load(bool hotReload)
    {
        API = new CS2GamingAPI(this);

        Capabilities.RegisterPluginCapability(APICapability, () => API);
    }

    public void OnConfigParsed(APIConfigs config)
    {
        Config = config;
        _cs2GamingHttpClient.Initialize(config);
    }

    [RequiresPermissions("@css/generic")]
    [CommandHelper(1, "css_api [userid]")]
    [ConsoleCommand("css_api")]
    public void APICommandAsync(CCSPlayerController controller, CommandInfo info)
    {
        var steamid = ulong.Parse(info.GetArg(1));

        Task.Run(async () =>
        {
            var response = await _cs2GamingHttpClient.RequestSteamID(steamid);

            if (response == null)
                return;

            Server.NextFrame(() =>
            {
                _logger.LogInformation($"Status Code: {response.Status}");
                _logger.LogInformation($"Message : {response.Message}");
            });
        });
    }
}
