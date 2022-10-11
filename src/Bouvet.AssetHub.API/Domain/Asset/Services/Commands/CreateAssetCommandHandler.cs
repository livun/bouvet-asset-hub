using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Option<AssetEntity>>
    {
        private readonly IAssetRepository _repository;

        public CreateAssetCommandHandler(IAssetRepository repository)
        {
            _repository = repository;

        }

        public async Task<Option<AssetEntity>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await  _repository.GetBySerialNumber(request.Asset.SerialNumber.Value);
            if ( asset.IsNone )
            {
                return await _repository.Add(request.Asset);
            }
            return Option<AssetEntity>.None;
            
        }
    }
}
