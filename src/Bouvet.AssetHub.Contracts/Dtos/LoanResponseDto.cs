namespace Bouvet.AssetHub.Contracts.Dtos
{
    public class LoanResponseDto
    {
        public int Id { get; set; }
        public DateTime IntervalStart { get; set; }
        public DateTime? IntervalStop { get; set; }
        public bool IntervalIsLongterm { get; set; }

        public int AssignedToValue { get; set; }
        public int AssetId { get; set; }
        public Status AssetStatus { get; set; }
        public string AssetCategoryName { get; set; } = "";
        public string BsdReference { get; set; } = "";
    }
}
