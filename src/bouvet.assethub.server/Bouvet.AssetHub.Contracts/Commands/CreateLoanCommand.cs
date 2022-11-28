using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public class CreateLoanCommand : IRequest<Option<LoanResponseDto>>
    {
        public DateTime IntervalStart { get; set; }
        public DateTime? IntervalStop { get; set; }
        public bool IntervalIsLongterm { get; set; }
        public int AssignedToValue { get; set; }
        public int AssetId { get; set; }
        public string BsdReference { get; set; } = "";
    }
}
