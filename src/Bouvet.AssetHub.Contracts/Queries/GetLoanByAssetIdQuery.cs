using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetLoanByAssetIdQuery(int Id) : IRequest<Option<LoanResponseDto>>;

}
