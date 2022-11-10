using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class DeleteLoanByIdCommandHandler : IRequestHandler<DeleteLoanByIdCommand, Option<LoanResponseDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanHistoryRepository _historyRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public DeleteLoanByIdCommandHandler(
            ILoanRepository loanRepository,
            ILoanHistoryRepository historyRepository,
            IAssetRepository assetRepository,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _historyRepository = historyRepository;
            _assetRepository = assetRepository;
            _mapper = mapper;

        }

        public async Task<Option<LoanResponseDto>> Handle(DeleteLoanByIdCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.Delete(request.Id);
            if (loan.IsNone) return null;

            var loanHistory = _mapper.Map<LoanEntity, LoanHistoryEntity>(loan.First());
            var result = await _historyRepository.Add(loanHistory);
            var asset = await _assetRepository.UpdateAssetStatus(loan.First().Asset.Id, Status.Available);

            return result.IsSome && asset.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(loan.First()) : null;

        }
    }
}
