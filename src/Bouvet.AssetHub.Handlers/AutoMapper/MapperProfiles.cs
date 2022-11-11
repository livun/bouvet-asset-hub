
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;

namespace Bouvet.AssetHub.Handlers.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {

            CreateMap<Status, Contracts.Status>()
                .ConvertUsingEnumMapping().ReverseMap();
            CreateMap<AssetEntity, CreateAssetCommand>().ReverseMap();
            CreateMap<AssetEntity, AssetResponseDto>().ReverseMap();
            CreateMap<AssetEntity, UpdateAssetCommand>().ReverseMap();

            CreateMap<CategoryEntity, CreateCategoryCommand>().ReverseMap();
            CreateMap<CategoryEntity, CategoryResponseDto>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryCommand>().ReverseMap();

            CreateMap<CreateLoanDto, CreateLoanCommand>();
            CreateMap<LoanEntity, CreateLoanCommand>().ReverseMap();
            CreateMap<LoanEntity, LoanResponseDto>().ReverseMap();
            CreateMap<LoanEntity, UpdateLoanByIdCommand>().ReverseMap();
            CreateMap<LoanEntity, LoanHistoryEntity>().ReverseMap();

            CreateMap<LoanHistoryEntity, LoanHistoryResponseDto>().ReverseMap();
        }
    }
}
