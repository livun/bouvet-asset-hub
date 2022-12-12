using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record CreateAssetCommand(string SerialNumberValue, int CategoryId, Guid QrIdentifierValue) : IRequest<Option<AssetResponseDto>>;
}