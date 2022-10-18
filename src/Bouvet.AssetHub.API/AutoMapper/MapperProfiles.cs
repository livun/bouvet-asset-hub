using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;

namespace Bouvet.AssetHub.API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<AssetEntity, CreateAssetCommand>().ReverseMap();
            CreateMap<AssetEntity, AssetResponseDto>().ReverseMap();
            CreateMap<AssetEntity, UpdateAssetCommand>().ReverseMap();
            CreateMap<CategoryEntity, CreateCategoryCommand>().ReverseMap();
            CreateMap<CategoryEntity, CategoryResponseDto>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryCommand>().ReverseMap();
        }
    }
}
