using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record DeleteCategoryCommand(int Id) : IRequest<Option<CategoryResponseDto>>;
}