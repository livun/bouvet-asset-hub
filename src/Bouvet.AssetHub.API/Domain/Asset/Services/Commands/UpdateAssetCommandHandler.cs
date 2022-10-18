﻿using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
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
                var dto = _mapper.Map<AssetEntity, AssetResponseDto>(result.First());
                return dto;
            }
       
            return Option<AssetResponseDto>.None;

        }
}
}
