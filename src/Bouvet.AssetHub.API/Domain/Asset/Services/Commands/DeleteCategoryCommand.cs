using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public record DeleteCategoryCommand (int Id) : IRequest<Option<CategoryResponseDto>>;
}
