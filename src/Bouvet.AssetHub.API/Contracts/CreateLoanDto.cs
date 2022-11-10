namespace Bouvet.AssetHub.API.Contracts
{
    public record CreateLoanDto (
        DateTime IntervalStart, 
        DateTime? IntervalStop, 
        Boolean IntervalIsLongterm,
        int AssignedToValue,
        int AssetId, 
        string BsdReference
        );
}
