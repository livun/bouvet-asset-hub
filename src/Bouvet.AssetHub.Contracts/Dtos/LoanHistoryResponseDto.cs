namespace Bouvet.AssetHub.Contracts.Dtos
{
    public class LoanHistoryResponseDto
    {
        public int Id { get; set; }
        public DateTime IntervalStart { get; set; }
        public DateTime IntervalStop { get; set; }
        public DateTime ReturnDate { get; set; }
        public int BorrowerEmployeeNumberValue { get; set; }
        public int AssetId { get; set; }

    }
}
