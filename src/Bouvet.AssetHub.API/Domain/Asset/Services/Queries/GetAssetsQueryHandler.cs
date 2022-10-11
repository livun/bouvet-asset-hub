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
 

        public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, List<AssetEntity>>
        {
            private readonly IAssetRepository _repository;

            public GetAssetsQueryHandler(IAssetRepository repository)
            {
                _repository = repository;

            }

            public async Task<List<AssetEntity>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
            {
            throw new NotImplementedException();

           

        }
    }


    public class Indetificator {
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
