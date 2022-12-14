using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Handlers.Helpers;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class GetAssetByQueryHandler : IRequestHandler<GetAssetByQuery, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetByQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Option<AssetResponseDto>> Handle(GetAssetByQuery query, CancellationToken cancellationToken)
        {
            var predicate = query.Id == null ? AssetPredicates.ByGuid((Guid)query.Guid!) : AssetPredicates.ById((int)query.Id!);
            var result = await _repository.Get(predicate);
            if (result.IsSome)
            {
                var dto = _mapper.Map<AssetEntity, AssetResponseDto>((AssetEntity)result);
                return dto;
            }
            return Option<AssetResponseDto>.None;
        }
    }
}