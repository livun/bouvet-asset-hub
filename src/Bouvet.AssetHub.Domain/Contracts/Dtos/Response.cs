using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Dtos
{
    public record Response<T>(bool Success, T Value);
    
}
