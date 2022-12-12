using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{


    public class GetLoansByEmployeeNumberQueryHandler : IRequestHandler<GetLoansByEmployeeNumberQuery, Option<List<LoanResponseDto>>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoansByEmployeeNumberQueryHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Option<List<LoanResponseDto>>> Handle(GetLoansByEmployeeNumberQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByEmployeeNumber(query.EmployeeNumber);
            return result.IsSome ? _mapper.Map<List<LoanEntity>, List<LoanResponseDto>>(result.First()) : null;
        }
    }
}