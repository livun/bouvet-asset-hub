using AutoMapper;
using Bouvet.AssetHub.Contracts.Commands;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Handlers
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Option<LoanResponseDto>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<LoanResponseDto>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loanEntity = _mapper.Map<CreateLoanCommand, LoanEntity>(request);
            var loan = await _repository.Add(loanEntity);
            if (loan.IsSome)
            {
                return _mapper.Map<LoanEntity, LoanResponseDto>(loan.First());
            }
            return Option<LoanResponseDto>.None;

        }
    }
}
