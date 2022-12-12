using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record UpdateLoanByIdCommand(int Id, DateTime IntervalStop) : IRequest<Option<LoanResponseDto>>;
}