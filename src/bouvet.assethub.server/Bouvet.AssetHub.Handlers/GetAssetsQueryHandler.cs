using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
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
        public async Task<Option<List<AssetResponseDto>>> Handle(GetAssetsQuery query, CancellationToken cancellationToken)
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