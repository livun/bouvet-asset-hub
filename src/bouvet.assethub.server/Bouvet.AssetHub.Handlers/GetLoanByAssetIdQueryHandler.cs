using AutoMapper;
using Bouvet.AssetHub.API.Helpers;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
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

        public async Task<Option<LoanResponseDto>> Handle(GetLoanByAssetIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(LoanPredicates.ByAssetId(query.Id));
            return result.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(result.First()) : null;
        }
    }
}