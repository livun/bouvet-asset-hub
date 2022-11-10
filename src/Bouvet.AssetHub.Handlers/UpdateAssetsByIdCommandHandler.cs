using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Helpers;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class UpdateAssetsByIdCommandHandler : IRequestHandler<UpdateAssetsByIdCommand, Option<List<AssetResponseDto>>>
    {
        private readonly IAssetRepository _repository;
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public UpdateAssetsByIdCommandHandler(IAssetRepository repository, ILoanRepository loanRepository, IMapper mapper)
        {
            _repository = repository;
            _loanRepository = loanRepository;
            _mapper = mapper;

        }

        public async Task<Option<List<AssetResponseDto>>> Handle(UpdateAssetsByIdCommand request, CancellationToken cancellationToken)
        {
            var updatedAssets = new List<AssetResponseDto>();
            foreach (var id in request.Ids)
            {
                var loan = await _loanRepository.Get(LoanPredicates.ByAssetId(id));
                if (loan.IsSome)
                {
                    continue;
                }
                var asset = await _repository.UpdateAssetStatus(id, request.Status);

                if (asset.IsSome)
                {
                    var dto = _mapper.Map<AssetEntity, AssetResponseDto>(asset.First());
                    updatedAssets.Add(dto);
                }
            }
            if (updatedAssets.Any()) return updatedAssets;
            return Option<List<AssetResponseDto>>.None;

        }
    }
}
