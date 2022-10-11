using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateAssetCommand : IRequest<Option<AssetEntity>>
    {
        public AssetEntity Asset { get; set; } = new AssetEntity();
    }
}
