
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
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoansController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
         
        }

        // GET /loans
        [HttpGet]
        public async Task<IActionResult> GetLoansAsync()
        {
                        throw new NotImplementedException();
        }

        // POST /loans
        [HttpPost]
        public async Task<IActionResult> AddLoanAsync()
        {
            //AssetEntity asset = _mapper.Map<AssetEntity>();
            //Console.WriteLine(asset);

            //return Ok(asset);
            throw new NotImplementedException();    

        }

        // GET /loans/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanByIdAsync(int id)
        {
            throw new NotImplementedException();

        }
        // PUT /loans/1
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateLoanById(int id)
        {
            throw new NotImplementedException(nameof(id));  
        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLoanById(int id)
        {
            throw new NotImplementedException();
        }

    }
 

}
