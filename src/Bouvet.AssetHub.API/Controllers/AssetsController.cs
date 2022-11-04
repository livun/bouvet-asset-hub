﻿using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using Bouvet.AssetHub.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IMediator mediator, ILogger<AssetsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET /assets
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetAssetsAsync()
        {
            var result = await _mediator.Send(new GetAssetsQuery());
            return new ActionResultHelper<List<AssetResponseDto>>().OkOrNotFound(result, "Currently no assets in table!");
        }

        // POST /assets
        [HttpPost]
        public async Task<ActionResult<AssetResponseDto>> AddAssetAsync(CreateAssetCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ActionResultHelper<AssetResponseDto>().OkOrBadRequest(result, "Could not add asset!");

        }
        // PUT /assets
        [HttpPut]
        public async Task<ActionResult<List<AssetResponseDto>>> UpdateAssetByIdAsync(UpdateAssetsByIdCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ActionResultHelper<List<AssetResponseDto>>().OkOrNotFound(result, "Cannot update assets!");
        }

        // GET /assets/1
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<AssetResponseDto>> GetAssetbyIdAsync(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery(id));
            return new ActionResultHelper<AssetResponseDto>().OkOrNotFound(result, "Asset does not exist!");

        }


        // PUT /assets/1
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<AssetResponseDto>> UpdateAssetAsync(UpdateAssetDto dto, int id)
        {
            var result = await _mediator.Send(new UpdateAssetCommand(id, dto.Status, dto.CategoryId));
            return new ActionResultHelper<AssetResponseDto>().OkOrNotFound(result, "Cannot update asset!");

        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<AssetResponseDto>> DeleteAssetByIdAsync(int id)
        {
            var result = await _mediator.Send(new DeleteAssetCommand(id));
            return new ActionResultHelper<AssetResponseDto>().OkOrBadRequest(result, "Asset cannot be deleted!");


        }
        // GET /assets/1/loans
        [Route("{id}/loans")]
        [HttpGet]
        public async Task<ActionResult<LoanResponseDto>> GetLoansByAssetIdAsync(int id)
        {
            var result = await _mediator.Send(new GetLoanByAssetIdQuery(id));
            return new ActionResultHelper<LoanResponseDto>().OkOrNotFound(result, $"There is no loan on asset with id {id}");

        }

       


    }
 

}
