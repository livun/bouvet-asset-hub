using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record UpdateLoanByIdCommand(int Id, DateTime IntervalStop) : IRequest<Option<LoanResponseDto>>;

}
