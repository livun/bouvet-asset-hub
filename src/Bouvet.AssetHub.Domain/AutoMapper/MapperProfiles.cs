using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using Bouvet.AssetHub.API.Domain.Loan.Services.Commands;

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

            CreateMap<CreateLoanDto, CreateLoanCommand>();
            CreateMap<LoanEntity, CreateLoanCommand>().ReverseMap();
            CreateMap<LoanEntity, LoanResponseDto>().ReverseMap();
            CreateMap<LoanEntity, UpdateLoanByIdCommand>().ReverseMap();
            CreateMap<LoanEntity, LoanHistoryEntity>().ReverseMap();

            CreateMap<LoanHistoryEntity, LoanHistoryResponseDto>().ReverseMap();
        }
    }
}
