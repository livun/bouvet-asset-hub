using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<Option<CategoryResponseDto>>;
}
