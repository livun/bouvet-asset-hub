using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using Bouvet.AssetHub.API.Helpers;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{


    public class GetLoanByAssetIdQueryHandler : IRequestHandler<GetLoanByAssetIdQuery, Option<LoanResponseDto>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoanByAssetIdQueryHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<LoanResponseDto>> Handle(GetLoanByAssetIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(LoanPredicates.ByAssetId(request.Id));
            return result.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(result.First()) : null;
        }
    }
}
