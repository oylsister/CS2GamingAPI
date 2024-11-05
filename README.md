# CS2GamingAPI
 Plugin sending async POST request to web endpoint.

 ## Requirement
 - [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/)
 - [Shop-Core](https://github.com/Ganter1234/Shop-Core) (For shop restriction module).
 - [VIP-Core](https://github.com/partiusfabaa/cs2-VIPCore) (For VIP restriction module)

 ## Building
 ### Method 1
 Simply open solution file in Visual Studio 2022 and click ``Build Solution``

 ### Method 2
 Build via Command Prompt or PowerShell (Required: .NET SDK 8.0+)
 1. ``git clone https://github.com/oylsister/CS2GamingAPI.git``
 2. ``cd CS2GamingAPI``
 3. ``dotnet build``

 ## Installation
 1. Drag files in zip to ``game/csgo`` folder.
 2. Configure all json file in ``game/csgo/addons/counterstrikesharp/configs/CS2GamingAPICore`` folder. (If not exists, the server will generate automatically on startup.)
 3. Enjoy :D

 ## Configure
 ```jsonc
{
  "Token": "", // 32 lenght token provided by CS2Gaming
  "ProjectUrl": "https://************.com/", // Your URL
  "ProjectUrlType": "steamgroup", // set to "steamgroup", "vk", "website", "telegram" any other than these will not work
  "RequestUrl": "https://************.com/*******", // request URL
  "ConfigVersion": 1 // no need to do anything.
}
 ```

 ## Plugin API Usage
 ```cs
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
 ```
 