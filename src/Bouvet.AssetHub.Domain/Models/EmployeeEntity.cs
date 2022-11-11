using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.Domain.Models
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
