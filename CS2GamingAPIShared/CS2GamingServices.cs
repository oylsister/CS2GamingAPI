namespace CS2GamingAPIShared
{
    public interface ICS2GamingAPIShared
    {
        public class CS2GamingResponse
        {
            public int Status { get; set; }
            public string? Message { get; set; }
            public string? Message_RU { get; set; }
        }

        public Task<CS2GamingResponse?> RequestSteamID(ulong steamID, string type = "steamgroup");
    }
}