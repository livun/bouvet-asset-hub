using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Contracts
{
    public record UpdateAssetDto (Status Status, int CategoryId);
}
