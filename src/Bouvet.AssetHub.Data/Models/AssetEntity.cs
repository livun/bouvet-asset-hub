using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Data.Models
{
    public class AssetEntity : Entity
    {
        public int? SerialNumber { get; set; }
        public Guid QrIdentifier { get; set; } = Guid.NewGuid(); 
        public DateTime CreatedAt { get; set; } // Date for when its added to system
        public Status Status { get; set; }
        public Category Category { get; set; }
        //public Details Details { get; set; }
    }

    public enum Status
    {
        Registered, // ny ressurs som akkurat er lagt til eller akkurat sjekket inn --> må kalrgjøre før den blir available
        Available, // klar og tilgjengelig for å sjekkes ut
        Unavailable, // utigjengelig, altså sjekket ut
        Discontinued // avviklet
    }
    //public class Details // Metadata for the different kinds of assets
    //{
    //    public Category Category { get; set; }
    //    public string Brand { get; set; }
    //    public string Model { get; set; }
    //    public string Description { get; set; } // If catgory is other, add description of asset category here or leave empty

    //}
    public enum Category /// category entity
    {
        DeveloperPC,
        UserPC,
        DeveloperMAC,
        UserMAC,
        ExamPC,
        Screen,
        Keyboard,
        Mouse,
        Headset,
        Other
    }
}
