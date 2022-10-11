using Bouvet.AssetHub.API.Domain.Asset.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bouvet.AssetHub.API.Contracts
{
    public class AssetRequestDto
    {
        public int SerialNumberValue { get; set; }
        public int CategoryId { get; set; }

    }
}
