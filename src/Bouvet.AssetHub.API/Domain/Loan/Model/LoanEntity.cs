using Bouvet.AssetHub.API.Domain.Employee.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Domain.Loan.Model
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
