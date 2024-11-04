using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using static CS2GamingAPIShared.ICS2GamingAPIShared;

namespace CS2GamingAPICore.Models;

public interface ICS2GamingServices
{
    void Initialize(APIConfigs configs);
    Task<CS2GamingResponse?> RequestSteamID(ulong clientID, string type = "steamgroup");
}

public class CS2GamingHttpClient : ICS2GamingServices
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CorePlugin> _logger;
    private APIConfigs? _apiConfigs;

    public CS2GamingHttpClient(HttpClient httpClient, ILogger<CorePlugin> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public void Initialize(APIConfigs apiConfigs)
    {
        _apiConfigs = apiConfigs;
        _httpClient.BaseAddress = new Uri(_apiConfigs.RequestUrl);
    }

    public async Task<CS2GamingResponse?> RequestSteamID(ulong clientID, string type = "steamgroup")
    {
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            token = _apiConfigs?.Token,
            project_url = _apiConfigs?.ProjectUrl,
            project_url_type = type,
            steamId = clientID,
        }), Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await _httpClient.PostAsync("", content);

        var status = (int)response.StatusCode;
        var jsonResponse = await response.Content.ReadAsStringAsync();

        _logger.LogInformation(jsonResponse);

        var result = JObject.Parse(jsonResponse);

        CS2GamingResponse webResponse = new();

        webResponse.Status = status;
        webResponse.Message = (string)result["message_en"]!;
        webResponse.Message_RU = (string)result["message_ru"]!;

        return webResponse;
    }
}

