using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Handlers.Helpers;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
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
            if (asset.IsNone || request.SerialNumberValue == 0)
            {
                var assetEntity = _mapper.Map<CreateAssetCommand, AssetEntity>(request);
                return _mapper.Map<AssetEntity, AssetResponseDto>((await _repository.Add(assetEntity)).First());
            }
            return Option<AssetResponseDto>.None;

        }
    }
}
