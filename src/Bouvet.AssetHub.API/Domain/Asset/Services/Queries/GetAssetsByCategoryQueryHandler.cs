using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Predicates;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
{
    public class GetAssetsByCategoryQueryHandler : IRequestHandler<GetAssetsByCategoryQuery, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetsByCategoryQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<AssetResponseDto>>> Handle(GetAssetsByCategoryQuery request, CancellationToken cancellationToken)
        {


            var result = await _repository.GetByCategory(request.CategoryId);
            if (result.IsSome)
            {
                return _mapper.Map<List<AssetEntity>, List<AssetResponseDto>>((List<AssetEntity>)result);
            }
            return Option<List<AssetResponseDto>>.None;

        }
    }
}
