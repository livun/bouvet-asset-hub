using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public DeleteAssetCommandHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetResponseDto>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            // valid to delete if asset has status 0
            var asset = await _repository.Delete(request.Id);
            if ( asset.IsSome )
            {

                return _mapper.Map<AssetEntity, AssetResponseDto>(asset.First());
            }
            return Option<AssetResponseDto>.None;
            
        }
    }
}
