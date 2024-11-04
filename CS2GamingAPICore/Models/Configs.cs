using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace CS2GamingAPICore.Models;

public class APIConfigs : BasePluginConfig
{
    [JsonPropertyName("token")]
    private string _token { get; set; } = string.Empty;

    [JsonPropertyName("project_url")]
    private string _project_url { get; set; } = string.Empty;

    [JsonPropertyName("project_url_type")]
    private string _project_url_type { get; set; } = string.Empty;

    [JsonPropertyName("request_url")]
    private string _request_url { get; set; } = string.Empty;

    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }

    public string ProjectUrl
    {
        get { return _project_url; }
        set { _project_url = value; }
    }

    public string ProjectUrlType
    {
        get { return _project_url_type; }
        set { _project_url_type = value; }
    }

    public string RequestUrl
    {
        get { return _request_url; }
        set { _request_url = value; }
    }
}
