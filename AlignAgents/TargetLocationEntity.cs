using System;
using Newtonsoft.Json;

public class TargetLocationEntity
{
    [JsonProperty("targetlocation")]
    public string TargetLocation { get; set; }
}