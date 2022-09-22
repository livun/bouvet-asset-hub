using Bouvet.AssetHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Dtos
{
    public record AssetResponse(int serialNumber, Guid QrIdentifier, Status status, string category);
    
}
