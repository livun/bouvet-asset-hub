using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Option<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<CategoryResponseDto>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<UpdateCategoryCommand, CategoryEntity>(command);
            var result = await _repository.Update(category);
            if (result.IsSome)
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(result.First());
            }
            return Option<CategoryResponseDto>.None;
        }
    }
}