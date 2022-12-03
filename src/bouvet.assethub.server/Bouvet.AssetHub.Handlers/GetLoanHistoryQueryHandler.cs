using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class GetLoanHistoryQueryHandler : IRequestHandler<GetLoanHistoryQuery, Option<List<LoanHistoryResponseDto>>>
    {
        private readonly ILoanHistoryRepository _repository;
        private readonly IMapper _mapper;

        public GetLoanHistoryQueryHandler(ILoanHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<LoanHistoryResponseDto>>> Handle(GetLoanHistoryQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAll();
            if (result.IsSome)
            {

                return _mapper.Map<List<LoanHistoryEntity>, List<LoanHistoryResponseDto>>(result.First());
            }

            return Option<List<LoanHistoryResponseDto>>.None;

        }
    }
}
