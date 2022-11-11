using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class Interval
    {
        public DateTime Start { get; set; } = DateTime.Today; // Date of hand-out - return by date
        public DateTime? Stop { get; set; } // Date of hand-in - if null - its not a temporary loan
        public bool IsLongterm { get; set; } = false;
    }
}
