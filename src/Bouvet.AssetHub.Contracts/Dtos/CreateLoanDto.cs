namespace Bouvet.AssetHub.Contracts.Dtos
{
    public record CreateLoanDto(
        DateTime IntervalStart,
        DateTime? IntervalStop,
        bool IntervalIsLongterm,
        int AssignedToValue,
        int AssetId,
        string BsdReference
        );
}
