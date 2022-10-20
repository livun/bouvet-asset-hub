using Bouvet.AssetHub.API.Domain.Asset.Models;

namespace Bouvet.AssetHub.API.Contracts
{
    public record UpdateAssetDto (Status Status, int CategoryId);
}
