using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Data.Models
{
    public class LoanEntity : Entity
    {
        private LoanEntity(){}
        public LoanEntity(AssetEntity asset, EmployeeEntity assignedTo)
        {
            Asset = asset;
            AssignedTo = assignedTo;
        }
        public DateTime CheckOut { get; set; } = DateTime.Now; // Date of hand-out - return by date
        public DateTime? CheckIn { get; set; } // Date of hand-in - if null - its not a temporary loan
        public EmployeeEntity AssignedTo { get; set; }
        public AssetEntity Asset { get; set; }
        public Bsd? BsdReference { get; set; } // if a request has been made through BSD, add reference 
    }

    public class Bsd : Entity

    {
        public string? Reference { get; set; } 
    }

   
}
