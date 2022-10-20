using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
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

        public async Task<Option<CategoryResponseDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           
            var categoryEntity = _mapper.Map<CreateCategoryCommand, CategoryEntity>(request);
            var result = await _repository.Add(categoryEntity);
            if (result.IsSome)
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(result.First());
            }           
            return Option<CategoryResponseDto>.None;
            
        }
    }
}
