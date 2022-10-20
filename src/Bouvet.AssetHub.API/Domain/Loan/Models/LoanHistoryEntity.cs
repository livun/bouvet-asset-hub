using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Employee.Models;
using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.API.Domain.Loan.Models
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
