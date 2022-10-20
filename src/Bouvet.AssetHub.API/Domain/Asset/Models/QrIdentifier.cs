using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Domain.Asset.Models
{
    [Owned]
    public class QrIdentifier
    {
        public Guid Value { get; set; } = new Guid();
    }
}