using Bouvet.AssetHub.Domain.Contracts.Events;
using Bouvet.AssetHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Contracts.Dtos
{
       public record AssetResponse(int? SerialNumber, Guid QrIdentifier, string Status, string Category);
    
}
