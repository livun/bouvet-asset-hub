using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetAssetsByCategoryQuery(int CategoryId) : IRequest<Option<List<AssetResponseDto>>>;
}