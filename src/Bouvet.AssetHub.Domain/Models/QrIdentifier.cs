using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class QrIdentifier
    {
        public Guid Value { get; set; } = new Guid();
    }
}