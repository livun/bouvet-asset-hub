using Bouvet.AssetHub.API.Domain.Asset.Models;

namespace Bouvet.AssetHub.API.Contracts
{
    public record UpdateAssetsByIdDto ( List<int> Ids, Status Status);
}
