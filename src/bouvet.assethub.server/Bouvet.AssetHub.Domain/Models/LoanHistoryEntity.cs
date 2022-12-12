using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.Domain.Models
{
    public class LoanHistoryEntity : Entity
    {
        public Interval Interval { get; set; } = new Interval();
        public DateTime ReturnDate { get; set; } = DateTime.Now;
        public EmployeeEntity Borrower { get; set; } = new EmployeeEntity();
        [Required]
        public AssetEntity Asset { get; set; } = new AssetEntity();
    }
}