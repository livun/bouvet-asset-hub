using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Employee.Models;
using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.API.Domain.Loan.Models
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
