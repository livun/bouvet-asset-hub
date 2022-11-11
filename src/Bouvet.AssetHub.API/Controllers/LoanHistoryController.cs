using Bouvet.AssetHub.API.Helpers;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanHistory : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanHistory(IMediator mediator)
        {
            _mediator = mediator;
         
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
