using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Helpers;
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
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Option<AssetResponseDto>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetByIdQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<AssetResponseDto>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.Get(AssetPredicates.ById(request.Id));
            if (result.IsSome)
            {
                var dto = _mapper.Map<AssetEntity, AssetResponseDto>((AssetEntity)result);
                return dto;
            }
            return Option<AssetResponseDto>.None;

        }
    }
}
