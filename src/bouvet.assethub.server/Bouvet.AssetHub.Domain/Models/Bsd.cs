using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class Bsd
    {
        [Required]
        public string Reference { get; set; } = "None"; // if a request has been made through BSD, add reference 
    }
}
