using Bouvet.AssetHub.API.Domain.Asset.Models;

namespace Bouvet.AssetHub.API.Contracts
{
    public class AssetResponseDto
    {
        public int Id { get; set; }
        public int SerialNumberValue { get; set; }
        public string CategoryName { get; set; } = "";
        public Status Status { get; set; }
    }
}