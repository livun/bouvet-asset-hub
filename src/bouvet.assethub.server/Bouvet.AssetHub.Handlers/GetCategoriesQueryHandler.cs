using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{


    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Option<List<CategoryResponseDto>>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Option<List<CategoryResponseDto>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAll();
            if (result.IsSome)
            {
                return _mapper.Map<List<CategoryEntity>, List<CategoryResponseDto>>(result.First());
            }
            return Option<List<CategoryResponseDto>>.None;
        }
    }
}