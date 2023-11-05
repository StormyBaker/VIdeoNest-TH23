using Newtonsoft.Json;

namespace VideoNestServer.JsonStructures
{
    public class DownloadRequest
    {
        [JsonProperty("url")]
        public string url {  get; set; }
    }
}
