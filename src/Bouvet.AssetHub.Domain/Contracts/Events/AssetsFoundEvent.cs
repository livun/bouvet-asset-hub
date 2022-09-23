using Bouvet.AssetHub.Domain.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Contracts.Events
{
    public class AssetsFoundEvent : IEvent
    {
        public List<AssetResponse> Assets { get; set; } = new();

        public void AddAssets(int? SerialNumber, Guid QrIdentifier, Status Status, string Category)
        {
            var asset = new AssetResponse(SerialNumber, QrIdentifier, Status.ToString(), Category);
            Assets.Add(asset);
        }

    }

}
