using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Option<CategoryResponseDto>>;

}
