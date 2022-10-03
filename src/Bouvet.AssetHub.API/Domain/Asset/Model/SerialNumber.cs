using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Domain.Asset.Model
{
    [Owned]
    public class SerialNumber
    {
        public int Value { get; set; } = 0;
    }
}