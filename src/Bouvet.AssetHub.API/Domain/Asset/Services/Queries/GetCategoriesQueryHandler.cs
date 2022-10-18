using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
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

        public async Task<Option<List<CategoryResponseDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
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
