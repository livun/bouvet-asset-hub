using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
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

        public async Task<Option<LoanResponseDto>> Handle(DeleteLoanByIdCommand command, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.Delete(command.Id);
            if (loan.IsNone) return null;
            var loanHistory = _mapper.Map<LoanEntity, LoanHistoryEntity>(loan.First());
            var result = await _historyRepository.Add(loanHistory);
            var asset = await _assetRepository.UpdateAssetStatus(loan.First().Asset.Id, (Domain.Models.Status.Available));
            return result.IsSome && asset.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(loan.First()) : null;
        }
    }
}