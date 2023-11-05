using Newtonsoft.Json;

namespace VideoNestServer.JsonStructures
{
    public class AccountCredentials
    {
        [JsonProperty("email")]
        public string email {  get; set; }
        [JsonProperty("password")]
        public string password {  get; set; }
    }
}
