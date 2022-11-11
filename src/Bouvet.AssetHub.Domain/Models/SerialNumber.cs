using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class SerialNumber
    {
        public int Value { get; set; } = 0;
    }
}