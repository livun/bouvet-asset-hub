namespace Bouvet.AssetHub.Contracts.Dtos
{
    public record CreateAssetDto(string SerialNumber, int CategoryId, Guid QrIdentifier);
}
