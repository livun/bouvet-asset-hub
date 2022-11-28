using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.Domain.Models
{
    public class CategoryEntity : Entity 
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
