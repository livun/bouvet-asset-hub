using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Option<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Option<CategoryResponseDto>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(query.Id);
            if (result.IsSome)
            {
                var dto = _mapper.Map<CategoryEntity, CategoryResponseDto>(result.First());
                return dto;
            }
            return Option<CategoryResponseDto>.None;
        }
    }
}