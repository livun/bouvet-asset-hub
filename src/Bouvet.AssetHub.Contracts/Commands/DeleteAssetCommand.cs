using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record DeleteAssetCommand(int Id) : IRequest<Option<AssetResponseDto>>;
}
