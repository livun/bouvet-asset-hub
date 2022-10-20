using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class UpdateAssetsByIdCommand : IRequest<Option<List<AssetResponseDto>>>
    {
        public List<int> Ids { get; set; } = new List<int>();
        public Status Status { get; set; }
    }
}
