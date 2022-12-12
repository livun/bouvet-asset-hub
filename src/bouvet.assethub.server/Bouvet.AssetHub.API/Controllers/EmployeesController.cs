using Bouvet.AssetHub.API.Helpers;
using Bouvet.AssetHub.Contracts.Dtos;
using Bouvet.AssetHub.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;    
        }
        //GET /employees/1/loans
        [Route("{number}/loans")]
        [HttpGet]
        public async Task<ActionResult<List<LoanResponseDto>>> GetLoansByEmployeeNumberAsync(int number)
        {
            var result = await _mediator.Send(new GetLoansByEmployeeNumberQuery(number));
            return new ActionResultHelper<List<LoanResponseDto>>().OkOrNotFound(result, $"No loans on employee number: {number}!");
        }
    }
}
