using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Contracts
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
