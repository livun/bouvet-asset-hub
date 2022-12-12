using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
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

        public async Task<Option<AssetResponseDto>> Handle(DeleteAssetCommand command, CancellationToken cancellationToken)
        {
            // valid to delete if asset has status 0
            var asset = await _repository.Delete(command.Id);
            if (asset.IsSome)
            {
                return _mapper.Map<AssetEntity, AssetResponseDto>(asset.First());
            }
            return Option<AssetResponseDto>.None;
        }
    }
}