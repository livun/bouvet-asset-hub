using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateCategoryCommand : IRequest<Option<CategoryResponseDto>>
    {
        public string Name { get; set; } = "";
    }
}
