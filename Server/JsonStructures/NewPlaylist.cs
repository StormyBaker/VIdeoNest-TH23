using Newtonsoft.Json;

namespace VideoNestServer.JsonStructures
{
    public class NewPlaylist
    {
        [JsonProperty("name")]
        public string name {  get; set; }
        [JsonProperty("description")]
        public string description{  get; set; }
    }
}
