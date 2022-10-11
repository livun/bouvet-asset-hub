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
 

        public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Option<AssetEntity>>
        {
            private readonly IAssetRepository _repository;

            public GetAssetByIdQueryHandler(IAssetRepository repository)
            {
                _repository = repository;

            }

            public async Task<Option<AssetEntity>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.Get(request.Id);
            }
    }
}
