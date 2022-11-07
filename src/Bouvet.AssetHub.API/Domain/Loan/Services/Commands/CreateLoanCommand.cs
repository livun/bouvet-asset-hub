using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Loan.Services.Commands
{
    public class CreateLoanCommand : IRequest<Option<LoanResponseDto>>
    {
        public DateTime IntervalStart { get; set; }
        public DateTime? IntervalStop { get; set; }
        public Boolean IntervalIsLongterm { get; set; }
        public int AssignedToValue { get; set; }
        public int AssetId { get; set; }
        public string BsdReference { get; set; } = "";
    }
}
