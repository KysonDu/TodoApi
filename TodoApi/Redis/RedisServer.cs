using Newtonsoft.Json;

namespace TodoApi.Redis
{
    public class RedisServer
    {
        [JsonProperty("server")]
        public string Server { get; set; }
    }
}
