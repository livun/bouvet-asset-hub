using Bouvet.AssetHub.Domain.Contracts;
using Bouvet.AssetHub.Domain.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Contracts.Events;
using Bouvet.AssetHub.Domain.Data;
using Bouvet.AssetHub.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Handlers
{
    public class GetAssets
    {
        public record Request() : IRequest<Option<List<AssetResponse>>>;

        public class Handler : IRequestHandler<Request, Option<List<AssetResponse>>>
        {
            public readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
                
            }

            public async Task<Option<List<AssetResponse>>> Handle(Request request, CancellationToken cancellationToken)
            {
                var assets = await context.Assets
                    .Include(a => a.Category)
                    .ToListAsync(cancellationToken);
                if (assets is null) 
                    return Option<List<AssetResponse>>.CreateEmpty();

                var assetList = new List<AssetResponse>();

                // create a mapper from model to dto
                foreach (var asset in assets)
                {
                    assetList.Add(
                        new AssetResponse(
                            asset.SerialNumber, asset.QrIdentifier, asset.Status.ToString(), asset.Category.Name
                            ));
                }
                return Option<List<AssetResponse>>.Create(assetList);


                //return assets is null ? Option<List<AssetResponse>>.CreateEmpty() : Option<List<AssetResponse>>.Create


            }

        }
    }
}
