namespace Bouvet.AssetHub.Contracts.Dtos
{
    public record LoanHistoryResponseDto
    (
        int Id,
         DateTime IntervalStart,
         DateTime IntervalStop,
         DateTime ReturnDate,
         int BorrowerEmployeeNumberValue,
         int AssetId
    );
}
