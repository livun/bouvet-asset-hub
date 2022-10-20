using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
{
    public record GetLoanByIdQuery(int Id) : IRequest<Option<LoanResponseDto>>;
    
}
