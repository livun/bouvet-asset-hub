using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using Bouvet.AssetHub.API.Helpers;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
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

        public async Task<Option<List<LoanResponseDto>>> Handle(GetLoansByEmployeeNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByEmployeeNumber(request.EmployeeNumber);
            return result.IsSome ? _mapper.Map<List<LoanEntity>, List<LoanResponseDto>>(result.First()) : null;
        }
    }
}
