using Bouvet.AssetHub.API.Domain.Asset.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
{
    public class GetAssetsQuery : IRequest<List<AssetEntity>>
    {
    }
}
