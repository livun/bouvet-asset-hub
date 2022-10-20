using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public record UpdateAssetCommand(int Id, Status Status, int CategoryId) : IRequest<Option<AssetResponseDto>>;

}
