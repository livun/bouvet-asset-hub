
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers.Helpers;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Repositories;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using LanguageExt;
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
        private readonly IMapper _mapper;

        public AssetsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
         
        }

        // GET /assets
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetAssetsAsync()
        {
            var result = await _mediator.Send(new GetAssetsQuery());
            return new ResultHelper<List<AssetResponseDto>>().OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssetAsync(CreateAssetCommand dto)
        {
            var response = await _mediator.Send(dto);
            if (response.IsSome)
            {
                return Ok("New asset is added!");
            }
            return BadRequest("Could not add asset");


        }
        // PUT /assets
        [HttpPut]
        public async Task<IActionResult> UpdateAssetByIdAsync(List<int> id)
        {
            throw new NotImplementedException();

        }

        // GET /assets/1
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<AssetResponseDto>> GetAssetbyIdAsync(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery(id));
            return new ResultHelper<AssetResponseDto>().OkOrNotFound(result);
            //return OkOrNotFound(result);

        }


        // PUT /assets/1
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAssetByIdAsync(int id)
        {
            throw new NotImplementedException();

        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssetByIdAsync(int id)
        {
            throw new NotImplementedException();

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
