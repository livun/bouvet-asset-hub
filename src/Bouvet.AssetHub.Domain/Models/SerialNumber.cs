using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class SerialNumber
    {
        public string Value { get; set; } = "";
    }
}