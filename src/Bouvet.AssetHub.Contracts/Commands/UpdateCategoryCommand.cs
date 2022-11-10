using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record UpdateCategoryCommand(int Id, string Name) : IRequest<Option<CategoryResponseDto>>;

}
