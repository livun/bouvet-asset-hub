using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
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
            Func<AssetEntity, bool> ById = (a => a.Id == request.Id);
            var result = await _repository.Get(ById);
            if (result.IsSome)
            {
                var dto = _mapper.Map<AssetEntity, AssetResponseDto>((AssetEntity)result);
                return dto;
            }

            return Option<AssetResponseDto>.None;

            //return await _repository.Get(request.Id);
        }
    }
}
