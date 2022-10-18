
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers.Helpers;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Repositories;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using LanguageExt.UnsafeValueAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetsController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        // GET /assets
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetAssetsAsync()
        {
            var result = await _mediator.Send(new GetAssetsQuery());
            return new ResultHelper<List<AssetResponseDto>>().OkOrNotFound(result);
        }

        // POST /assets
        [HttpPost]
        public async Task<ActionResult<AssetResponseDto>> AddAssetAsync(CreateAssetCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ResultHelper<AssetResponseDto>().OkOrBadRequest(result, "Could not add asset!");

        }
        // PUT /assets
        [HttpPut]
        public async Task<ActionResult<List<AssetResponseDto>>> UpdateAssetByIdAsync(UpdateAssetsByIdCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ResultHelper<List<AssetResponseDto>>().OkOrNotFound(result);
        }

        // GET /assets/1
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<AssetResponseDto>> GetAssetbyIdAsync(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery(id));
            return new ResultHelper<AssetResponseDto>().OkOrNotFound(result);

        }


        // PUT /assets/1
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<AssetResponseDto>> UpdateAssetAsync(UpdateAssetDto dto, int id)
        {
            var result = await _mediator.Send(new UpdateAssetCommand(id, dto.Status, dto.CategoryId));
            return new ResultHelper<AssetResponseDto>().OkOrNotFound(result);

        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<AssetResponseDto>> DeleteAssetByIdAsync(int id)
        {
            var result = await _mediator.Send(new DeleteAssetCommand(id));
            return new ResultHelper<AssetResponseDto>().OkOrBadRequest(result, "Asset cannot be deleted!");


        }
        // GET /assets/1/loans
        [Route("{id}/loans")]
        [HttpGet]
        public async Task<IActionResult> GetLoansByAssetIdAsync(int id)
        {
            throw new NotImplementedException();

        }

       


    }
 

}
