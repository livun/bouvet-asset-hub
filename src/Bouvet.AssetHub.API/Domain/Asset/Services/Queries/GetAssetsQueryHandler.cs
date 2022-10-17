﻿using AutoMapper;
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


    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public GetAssetsQueryHandler(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<AssetResponseDto>>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAll();
            if (result.IsSome)
            {
               
                return _mapper.Map<List<AssetEntity>, List<AssetResponseDto>>((List<AssetEntity>)result);
            }

            return Option<List<AssetResponseDto>>.None;




        }
    }


    public class Indetificator
    {
        public int Id { get; set; }
    }

    public class IndetificatorValidator : AbstractValidator<Indetificator>
    {
        public IndetificatorValidator()
        {

        }
    }

    public class Assets<T> : List<T>
    {

    }
}
