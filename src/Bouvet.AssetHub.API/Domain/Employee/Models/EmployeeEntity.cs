using Bouvet.AssetHub.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Employee.Models
{

    public class EmployeeEntity : Entity
    {
        [Required]
        public EmployeeNumber EmployeeNumber { get; set; } = new EmployeeNumber();
    }

    [Owned]
    public class EmployeeNumber 
    {
        public EmployeeNumber()
        {
            Value = 0;
        }
        public int Value { get; set; }

    }
}
