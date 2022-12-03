using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetAssetByQuery(int? Id, Guid? Guid) : IRequest<Option<AssetResponseDto>>;

}

