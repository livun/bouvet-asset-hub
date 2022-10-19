using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Loan.Services.Commands
{
    public record UpdateLoanByIdCommand (int Id, DateTime IntervalStop) : IRequest<Option<LoanResponseDto>>;
   
}
