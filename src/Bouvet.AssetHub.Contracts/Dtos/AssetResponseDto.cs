using System.Text.Json.Serialization;

namespace Bouvet.AssetHub.Contracts.Dtos
{
    public class AssetResponseDto
    {
        public int Id { get; set; }
        public string SerialNumberValue { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}