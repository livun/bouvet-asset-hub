﻿
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using Bouvet.AssetHub.API.Domain.Loan.Services.Commands;
using Bouvet.AssetHub.API.Helpers;
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
        public async Task<ActionResult<List<LoanResponseDto>>> GetLoansAsync()
        {
            var result = await _mediator.Send(new GetLoansQuery());
            return new ActionResultHelper<List<LoanResponseDto>>().OkOrNotFound(result);
        }

        // POST /loans
        [HttpPost]
        public async Task<ActionResult<LoanResponseDto>> AddLoanAsync(CreateLoanCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ActionResultHelper<LoanResponseDto>().OkOrBadRequest(result, "Could not add loan!");    

        }

        // GET /loans/id
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanResponseDto>> GetLoanByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetLoanByIdQuery(id));
            return new ActionResultHelper<LoanResponseDto>().OkOrNotFound(result);

        }
        // PUT /loans/1
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<LoanResponseDto>> UpdateLoanById(UpdateLoanDto dto, int id)
        {
            var result = await _mediator.Send(new UpdateLoanByIdCommand(id, dto.IntervalStop));
            return new ActionResultHelper<LoanResponseDto>().OkOrNotFound(result);
        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<LoanResponseDto>> DeleteLoanById(int id)
        {
            var result = await _mediator.Send(new DeleteLoanByIdCommand(id));
            return new ActionResultHelper<LoanResponseDto>().OkOrBadRequest(result, "Could not delete loan!");


        }

    }
 

}
