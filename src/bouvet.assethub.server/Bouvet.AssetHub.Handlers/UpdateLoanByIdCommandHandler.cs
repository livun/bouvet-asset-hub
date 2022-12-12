using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
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

        public async Task<Option<LoanResponseDto>> Handle(UpdateLoanByIdCommand command, CancellationToken cancellationToken)
        {
            var loanEntity = _mapper.Map<UpdateLoanByIdCommand, LoanEntity>(command);
            var loan = await _repository.Update(loanEntity);
            return loan.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(loan.First()) : null;
        }
    }
}