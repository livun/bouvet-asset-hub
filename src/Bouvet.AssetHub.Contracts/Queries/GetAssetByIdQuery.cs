using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetAssetByIdQuery(int Id) : IRequest<Option<AssetResponseDto>>;

}
