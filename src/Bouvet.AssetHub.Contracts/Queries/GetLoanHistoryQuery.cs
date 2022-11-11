using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetLoanHistoryQuery : IRequest<Option<List<LoanHistoryResponseDto>>>;

}
