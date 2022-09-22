using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Models
{
    public class AssetEntity : Entity
    {
        public int? SerialNumber { get; set; }
        public Guid QrIdentifier { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date for when its added to system
        public Status Status { get; set; } = Status.Registered;

        [Required]
        public CategoryEntity Category { get; set; } = new CategoryEntity();
    }

    
  
    
}
