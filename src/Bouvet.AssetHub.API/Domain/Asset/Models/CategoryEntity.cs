using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.API.Domain.Asset.Models
{
    public class CategoryEntity : Entity 
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
