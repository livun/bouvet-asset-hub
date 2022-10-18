using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public record DeleteAssetCommand (int Id) : IRequest<Option<AssetResponseDto>>;
}
