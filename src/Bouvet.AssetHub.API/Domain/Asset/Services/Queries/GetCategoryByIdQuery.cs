using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Option<CategoryResponseDto>>;
  
}
