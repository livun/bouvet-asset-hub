using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAssetCommandHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetResponseDto>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {

            var asset = _mapper.Map<UpdateAssetCommand, AssetEntity>(request);
            var result = await _repository.Update(asset);

            if (result.IsSome)
            {
                return _mapper.Map<AssetEntity, AssetResponseDto>(result.First());
            }

            return Option<AssetResponseDto>.None;

        }
    }
}
