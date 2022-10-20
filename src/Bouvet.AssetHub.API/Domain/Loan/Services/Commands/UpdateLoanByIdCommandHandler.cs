using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Loan.Services.Commands
{
    public class UpdateLoanByIdCommandHandler : IRequestHandler<UpdateLoanByIdCommand, Option<LoanResponseDto>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public UpdateLoanByIdCommandHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<LoanResponseDto>> Handle(UpdateLoanByIdCommand request, CancellationToken cancellationToken)
        {
            var loanEntity = _mapper.Map<UpdateLoanByIdCommand, LoanEntity>(request);
            var loan = await _repository.Update(loanEntity);
           
            return  loan.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(loan.First()) : null;
            
        }
    }
}
