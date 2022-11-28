using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Bouvet.AssetHub.Contracts.Dtos
{
    public record LoanResponseDto
    {
        public int Id { get; set; }
        public DateTime IntervalStart { get; set; }
        public DateTime? IntervalStop { get; set; }
        public bool IntervalIsLongterm { get; set; }

        public int AssignedToValue { get; set; }
        public int AssetId { get; set; }

        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status AssetStatus { get; set; }
        public string AssetCategoryName { get; set; } = "";
        public string BsdReference { get; set; } = "";
    }
}
