
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetLoanHistoryAsync()
        {
            throw new NotImplementedException();
                   
        }
    }

}
