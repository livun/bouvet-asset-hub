using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Employee.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Loan.Model
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
