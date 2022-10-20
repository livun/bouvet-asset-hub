﻿using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.API.Domain.Loan.Services.Commands
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
            if ( loan.IsSome )
            {
                return _mapper.Map<LoanEntity, LoanResponseDto>(loan.First());
            }
            return Option<LoanResponseDto>.None;
            
        }
    }
}
