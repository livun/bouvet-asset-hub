using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Contracts
{
    public class LoanResponseDto
    {
        public int Id { get; set; }
        public DateTime IntervalStart { get; set; }
        public DateTime IntervalStop { get; set; }
        public Boolean IntervalIsLongterm { get; set; }

        public int AssignedToValue { get; set; }
        public int AssetId { get; set; }
        public Status AssetStatus { get; set; }
        public string AssetCategoryName { get; set; } = "";
        public string BsdReference { get; set; } = "";
    }
}
