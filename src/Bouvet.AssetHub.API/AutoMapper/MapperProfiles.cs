using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<AssetRequestDto, AssetEntity>();
            CreateMap<AssetEntity, AssetResponseDto>();
        }
    }
}
