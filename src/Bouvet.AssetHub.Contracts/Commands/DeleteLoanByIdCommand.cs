using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record DeleteLoanByIdCommand(int Id) : IRequest<Option<LoanResponseDto>>;

}
