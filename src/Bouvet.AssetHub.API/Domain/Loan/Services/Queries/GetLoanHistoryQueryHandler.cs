using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Services.Queries
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
