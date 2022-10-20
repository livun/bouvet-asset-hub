
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
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
         
        }

        //GET /employees/1/loans
        [Route("{number}/loans")]
        [HttpGet]
        public async Task<ActionResult<List<LoanResponseDto>>> GetLoansByEmployeeIdAsync(int number)
        {
            var result = await _mediator.Send(new GetLoansByEmployeeNumberQuery(number));
            return new ActionResultHelper<List<LoanResponseDto>>().OkOrNotFound(result);


        }




    }
 

}
