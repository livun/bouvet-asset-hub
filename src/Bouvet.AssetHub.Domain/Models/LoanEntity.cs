using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.Domain.Models
{
    public class LoanEntity : Entity
    {
        public Interval Interval { get; set; } = new Interval();
        [Required]
        public EmployeeNumber AssignedTo { get; set; } = new EmployeeNumber();
        public EmployeeEntity Borrower { get; set; } = new EmployeeEntity();
        [Required]
        public int AssetId { get; set; }
        public AssetEntity Asset { get; set; } = new AssetEntity();
        public Bsd? Bsd { get; set; }
    }




}
