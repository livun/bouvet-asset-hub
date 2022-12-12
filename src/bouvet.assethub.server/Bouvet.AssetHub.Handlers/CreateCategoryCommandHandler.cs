using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Option<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Option<CategoryResponseDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<CreateCategoryCommand, CategoryEntity>(command);
            var result = await _repository.Add(categoryEntity);
            if (result.IsSome)
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(result.First());
            }
            return Option<CategoryResponseDto>.None;
        }
    }
}