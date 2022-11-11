using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public class GetAssetsQuery : IRequest<Option<List<AssetResponseDto>>>
    {
    }
}
