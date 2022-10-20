using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Models
{
    public class AssetEntity : Entity
    {
        public SerialNumber SerialNumber { get; set; } = new SerialNumber();
        public QrIdentifier QrIdentifier { get; set; } = new QrIdentifier();
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date for when its added to system
        public Status Status { get; set; } = Status.Registered;

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = new CategoryEntity();
    }

    
  
    
}
