using Bouvet.AssetHub.API.Domain.Asset.Model;
using System.ComponentModel;

namespace Bouvet.AssetHub.API.Contracts
{
    public class AssetRequestDto
    {
        [DisplayName("SerialNumber")]
        public int SerialNumberValue { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; } = "";

    }
}
