using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Loan.Services.Commands
{
    public record DeleteLoanByIdCommand (int Id) : IRequest<Option<LoanResponseDto>>;
   
}
