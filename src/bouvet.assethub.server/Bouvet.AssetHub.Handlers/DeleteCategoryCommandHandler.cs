using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Option<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<CategoryResponseDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.Delete(request.Id);
            if (category.IsSome)
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(category.First());
            }
            return Option<CategoryResponseDto>.None;

        }
    }
}
