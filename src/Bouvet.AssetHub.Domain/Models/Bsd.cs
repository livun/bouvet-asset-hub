using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Models
{
    [Owned]
    public class Bsd
    {
        public string? Reference { get; set; } // if a request has been made through BSD, add reference 

    }
}
