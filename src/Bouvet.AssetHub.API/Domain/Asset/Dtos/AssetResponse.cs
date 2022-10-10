using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Dtos
{
       public record AssetResponse(int? SerialNumber, Guid QrIdentifier, string Status, string Category);
    
}
