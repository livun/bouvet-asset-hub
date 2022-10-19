using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using Bouvet.AssetHub.API.Helpers;
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


    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, Option<LoanResponseDto>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Option<LoanResponseDto>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(LoanPredicates.ById(request.Id));
            return result.IsSome ? _mapper.Map<LoanEntity, LoanResponseDto>(result.First()) : null;
        }
    }
}
