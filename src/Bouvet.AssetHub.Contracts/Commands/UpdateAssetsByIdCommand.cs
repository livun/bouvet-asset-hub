using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record UpdateAssetsByIdCommand(List<int> Ids, Status Status) : IRequest<Option<List<AssetResponseDto>>>;

}
