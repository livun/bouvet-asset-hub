using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Contracts
{
    public class AssetResponseDto
    {
        public int SerialNumberValue { get; set; }
        public string CategoryName { get; set; } = "";
        public Status Status { get; set; }
    }
}