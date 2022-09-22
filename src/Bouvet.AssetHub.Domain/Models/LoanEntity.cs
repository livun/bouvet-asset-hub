using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Models
{
    public class LoanEntity : Entity
    {
        public Interval Interval { get; set; } = new Interval();
        [Required]
        public EmployeeEntity AssignedTo { get; set; } = new EmployeeEntity();
        [Required]
        public AssetEntity Asset { get; set; } = new AssetEntity();
        public Bsd? Bsd { get; set; }
    }

    

   
}
