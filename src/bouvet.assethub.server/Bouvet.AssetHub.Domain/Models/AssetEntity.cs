using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bouvet.AssetHub.Domain.Models
{
    public class AssetEntity : Entity
    {
        public SerialNumber SerialNumber { get; set; }
        public QrIdentifier QrIdentifier { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date for when its added to system
        public Status Status { get; set; } = Status.Registered;
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } 
    }
}
