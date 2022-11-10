using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Queries
{
    public record GetLoansByEmployeeNumberQuery(int EmployeeNumber) : IRequest<Option<List<LoanResponseDto>>>;

}
