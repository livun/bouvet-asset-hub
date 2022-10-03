using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Loan.Model
{
    [Owned]
    public class Bsd
    {
        [Required]
        public string Reference { get; set; } = "None"; // if a request has been made through BSD, add reference 

    }
}
