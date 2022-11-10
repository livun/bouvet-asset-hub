using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Domain.Asset.Models
{
    [Owned]
    public class SerialNumber
    {
        public int Value { get; set; } = 0;
    }
}