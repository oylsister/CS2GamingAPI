using CS2GamingAPIShared;
using static CS2GamingAPIShared.ICS2GamingAPIShared;

namespace CS2GamingAPICore.Models;

public class CS2GamingAPI : ICS2GamingAPIShared
{
    CorePlugin _plugin;

    public CS2GamingAPI(CorePlugin plugin)
    {
        _plugin = plugin;
    }

    public async Task<CS2GamingResponse?> RequestSteamID(ulong steamID, string type = "steamgroup")
    {
        var request = await _plugin._cs2GamingHttpClient.RequestSteamID(steamID, type);

        if (request == null)
            return null;

        return request;
    }
}

