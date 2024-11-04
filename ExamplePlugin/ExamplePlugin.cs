using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CS2GamingAPIShared;

namespace ExamplePlugin
{
    public class ExamplePlugin : BasePlugin
    {
        public override string ModuleName => "Example Plugin for API";
        public override string ModuleVersion => "1.0";

        // create plugin capability
        public static PluginCapability<ICS2GamingAPIShared> APICapabillity { get; } = new("cs2gamingAPI");

        // declare API class.
        ICS2GamingAPIShared? API;

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            // get API from capabillity.
            API = APICapabillity.Get();
        }

        [RequiresPermissions("@css/generic")]
        [CommandHelper(1, "css_exam [userid]")]
        [ConsoleCommand("css_exam")]
        public async void APICommandAsync(CCSPlayerController controller, CommandInfo info)
        {
            // use it on anywhere start with API class you have declared.
            var request = await API!.RequestSteamID(ulong.Parse(info.GetArg(1)));

            if (request == null)
                return;

            Server.NextFrame(() =>
            {
                info.ReplyToCommand($"Status Code: {request.Status}");
                info.ReplyToCommand($"Message : {request.Message}");
            });
        }
    }
}
