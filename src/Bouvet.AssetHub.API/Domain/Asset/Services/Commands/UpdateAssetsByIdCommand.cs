using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public record UpdateAssetsByIdCommand (List<int> Ids, Status Status) : IRequest<Option<List<AssetResponseDto>>>;

}
