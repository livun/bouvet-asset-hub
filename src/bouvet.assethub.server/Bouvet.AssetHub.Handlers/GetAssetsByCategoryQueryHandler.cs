using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class GetAssetsByCategoryQueryHandler : IRequestHandler<GetAssetsByCategoryQuery, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetsByCategoryQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Option<List<AssetResponseDto>>> Handle(GetAssetsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByCategory(query.CategoryId);
            if (result.IsSome)
            {
                return _mapper.Map<List<AssetEntity>, List<AssetResponseDto>>((List<AssetEntity>)result);
            }
            return Option<List<AssetResponseDto>>.None;
        }
    }
}