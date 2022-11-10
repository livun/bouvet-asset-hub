using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{


    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetsQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<AssetResponseDto>>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAll();
            if (result.IsSome)
            {

                return _mapper.Map<List<AssetEntity>, List<AssetResponseDto>>(result.First());
            }

            return Option<List<AssetResponseDto>>.None;




        }
    }
}
