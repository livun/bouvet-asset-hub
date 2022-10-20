using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateAssetCommand : IRequest<Option<AssetResponseDto>>
    {
        public int SerialNumberValue { get; set; }
        public int CategoryId { get; set; }
    }
}
