using System;
using Newtonsoft.Json;

public class MissionEntity
{
    [JsonProperty("agent")]
    public string Agent { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }
}