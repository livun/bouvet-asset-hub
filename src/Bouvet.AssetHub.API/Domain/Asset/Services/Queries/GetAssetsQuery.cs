using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
{
    public class GetAssetsQuery : IRequest<Option<List<AssetResponseDto>>>
    {
    }
}
