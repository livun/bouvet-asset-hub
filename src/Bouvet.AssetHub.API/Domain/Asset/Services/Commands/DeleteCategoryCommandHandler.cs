using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Predicates;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
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
            if ( category.IsSome )
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(category.First());
            }
            return Option<CategoryResponseDto>.None;
            
        }
    }
}
