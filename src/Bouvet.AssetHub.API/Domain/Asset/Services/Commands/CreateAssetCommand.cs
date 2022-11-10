using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public record CreateAssetCommand (int SerialNumberValue, int CategoryId) : IRequest<Option<AssetResponseDto>>;

}
