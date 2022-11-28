using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record DeleteLoanByIdCommand(int Id) : IRequest<Option<LoanResponseDto>>;

}
