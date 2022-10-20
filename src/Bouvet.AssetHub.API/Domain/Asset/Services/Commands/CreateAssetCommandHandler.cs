using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Helpers;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public CreateAssetCommandHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetResponseDto>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _repository.Get(AssetPredicates.BySerialNumber(request.SerialNumberValue));
            if ( asset.IsNone || request.SerialNumberValue == 0 )
            {
                var assetEntity = _mapper.Map<CreateAssetCommand, AssetEntity>(request);
                return _mapper.Map<AssetEntity, AssetResponseDto>((await _repository.Add(assetEntity)).First());
            }
            return Option<AssetResponseDto>.None;
            
        }
    }
}
