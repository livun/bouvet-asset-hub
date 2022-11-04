
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using Bouvet.AssetHub.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanHistory : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoanHistory(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
         
        }

        // GET /loanhistory
        [HttpGet]
        public async Task<ActionResult<List<LoanHistoryResponseDto>>> GetLoansAsync()
        {
            var result = await _mediator.Send(new GetLoanHistoryQuery());
            return new ActionResultHelper<List<LoanHistoryResponseDto>>().OkOrNotFound(result, "Currently loan history table is empty.");
        }
    }

}
