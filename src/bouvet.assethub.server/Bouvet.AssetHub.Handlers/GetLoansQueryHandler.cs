using AutoMapper;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{


    public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, Option<List<LoanResponseDto>>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoansQueryHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<List<LoanResponseDto>>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetAll();
            if (result.IsSome)
            {

                return _mapper.Map<List<LoanEntity>, List<LoanResponseDto>>(result.First());
            }

            return Option<List<LoanResponseDto>>.None;




        }
    }
}
