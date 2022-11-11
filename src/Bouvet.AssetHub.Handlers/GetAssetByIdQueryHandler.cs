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
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetByIdQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetResponseDto>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.Get(AssetPredicates.ById(request.Id));
            if (result.IsSome)
            {
                var dto = _mapper.Map<AssetEntity, AssetResponseDto>((AssetEntity)result);
                return dto;
            }
            return Option<AssetResponseDto>.None;

        }
    }
}
