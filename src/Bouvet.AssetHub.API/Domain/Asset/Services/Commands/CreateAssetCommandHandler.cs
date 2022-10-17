using AutoMapper;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Option<AssetEntity>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public CreateAssetCommandHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetEntity>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            Func<AssetEntity, bool> BySerialNumber = (a => a.SerialNumber.Value == request.SerialNumberValue);
            var asset = await  _repository.Get(BySerialNumber);
            if ( asset.IsNone || request.SerialNumberValue == 0 )
            {
                var assetEntity = _mapper.Map<AssetEntity>(request);
                return await _repository.Add(assetEntity);
            }
            return Option<AssetEntity>.None;
            
        }
    }
}
