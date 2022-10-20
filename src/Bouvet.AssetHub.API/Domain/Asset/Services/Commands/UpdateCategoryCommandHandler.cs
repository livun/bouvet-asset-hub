using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Commands
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

        public async Task<Option<CategoryResponseDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = _mapper.Map<UpdateCategoryCommand, CategoryEntity>(request);
            var result = await _repository.Update(category);

            if (result.IsSome)
            {
                return _mapper.Map<CategoryEntity, CategoryResponseDto>(result.First());
            }

            return Option<CategoryResponseDto>.None;

        }
}
}
