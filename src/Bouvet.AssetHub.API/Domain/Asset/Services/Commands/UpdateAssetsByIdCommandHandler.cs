using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class UpdateAssetsByIdCommandHandler : IRequestHandler<UpdateAssetsByIdCommand, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAssetsByIdCommandHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<AssetResponseDto>>> Handle(UpdateAssetsByIdCommand request, CancellationToken cancellationToken)
        {
            var updatedAssets = new List<AssetResponseDto>();
            foreach(var id in request.Ids)
            {
                var asset = await _repository.UpdateAssetStatus(id, request.Status);

                if (asset.IsSome)
                {
                    var dto = _mapper.Map<AssetEntity, AssetResponseDto>(asset.First());
                    updatedAssets.Add(dto);
                }
            }
            if (updatedAssets.Any()) return updatedAssets;
            return Option<List<AssetResponseDto>>.None;

        }
    }
}
