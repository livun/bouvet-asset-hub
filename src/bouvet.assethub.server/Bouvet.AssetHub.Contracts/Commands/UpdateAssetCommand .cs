using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record UpdateAssetCommand(int Id, Status Status, int CategoryId) : IRequest<Option<AssetResponseDto>>;

}
