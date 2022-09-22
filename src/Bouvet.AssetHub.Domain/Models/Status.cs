using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Models
{
    public enum Status
    {
        Registered, // ny ressurs som akkurat er lagt til eller akkurat sjekket inn --> må kalrgjøre før den blir available
        Available, // klar og tilgjengelig for å sjekkes ut
        Unavailable, // utigjengelig, altså sjekket ut
        Discontinued // avviklet
    }
}
