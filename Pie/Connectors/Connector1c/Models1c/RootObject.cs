using System.Text.Json.Serialization;

namespace Pie.Connectors.Connector1c.Models1c
{
    public class RootObject<T>
    {
        [JsonPropertyName("odatametadata")]
        public string? OdataMetadata { get; set; }

        [JsonPropertyName("value")]
        public List<T>? Value { get; set; }
    }
}
